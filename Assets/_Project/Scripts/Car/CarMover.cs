using Dreamteck;
using System.Collections;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace Runtime.BaseCar
{
    public class CarMover : MonoBehaviour, IRespawnable
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Converter _converter;
        [SerializeField] private WheelsHandler _wheels;
        [SerializeField] private CenterOfMassPosition _centerOfMassPosition = new CenterOfMassPosition();
        [SerializeField] private PositionProperty _positionProperty;
        [SerializeField] private AnimationCurve _angularDragCurve;
        [SerializeField] private CarController _carController;

        [field: SerializeField] public float StopSpeed { get; private set; } = 200f;
        [field: SerializeField] public float MaxStopSpeed { get; private set; } = 75f;

        private AngularVelocityCalculator _angularDragCalculator;
        private CarRespawn _carRespawn;
        private bool _needToMove;
        private bool _itsMovingTime;

        private readonly float _relaxTime = 0.5f;

        public float MaxFlySpeed { get; private set; } = 50f;
        public float ChargedSpeed { get; private set; }
        public float LastChargedSpeed { get; private set; }
        public float RBVelocity => _rigidBody.velocity.magnitude;
        public float MaxSpeed => _converter.MaxForce;
        public bool IsMoving => _rigidBody.velocity.magnitude > 0.01f;
        public bool CanWRUMWRUM => _rigidBody.velocity.magnitude < 10f && _wheels.IsGrounded;

        public WheelsHandler Wheels => _wheels;

        private void Awake()
        {
            _carRespawn = new CarRespawn(_rigidBody, transform, _wheels);
            _angularDragCalculator = new AngularVelocityCalculator(0, 3,_rigidBody, _angularDragCurve, _wheels);
            _centerOfMassPosition.Init(_positionProperty);
        }

        private void Update()
        {
            if (_carRespawn.IsRespawning)
                return;

            MaxFlySpeed = _wheels.MaxFlySpeed;
            _angularDragCalculator.Update(_wheels.MaxAngularDrag);
            _wheels.Update();
            _rigidBody.centerOfMass = _centerOfMassPosition.GetCenterOfMassPosition(_wheels.IsGrounded);
            _rigidBody.angularDrag = _angularDragCalculator.Calculate(_rigidBody.velocity.magnitude, MaxSpeed);

            if (_playerInput.IsButtonDown)
            {
                if (_positionProperty.IsOnCarcase)
                    Respawn(transform.position + Vector3.up * 2, Quaternion.Euler(0, transform.eulerAngles.y, 0));

                _carController.SetStartRotation();
            }

            if (_playerInput.IsButtonUp && CanWRUMWRUM)
            {
                _needToMove = true;
            }

            if (_playerInput.IsButtonHold && _wheels.IsGrounded)
            {
                ChargedSpeed = _converter.ConvertYDelta();
            }

            if(_playerInput.IsButtonHold && _wheels.IsGrounded)
            {
                if(CanWRUMWRUM)
                    _carController.Rotate(_playerInput, ChargedSpeed, MaxSpeed);
                else if(IsMoving)
                    _carController.Rotate(_playerInput, _rigidBody.velocity.magnitude, MaxSpeed);

            }

            if (IsMoving)
            {
                float speed = MaxSpeed / _relaxTime;
                ChargedSpeed = Mathf.MoveTowards(ChargedSpeed, 0, speed * Time.deltaTime);
            }

        }

        private void FixedUpdate()
        {
            if (_wheels.IsGrounded)
            {
                float gripForce = Mathf.Lerp(0, _wheels.MaxGrip, RBVelocity / MaxSpeed);
                _rigidBody.AddForce(-transform.up * gripForce, ForceMode.Acceleration);
            }

            if (_playerInput.IsButtonHold && _wheels.IsGrounded && _rigidBody.velocity.magnitude < MaxStopSpeed && _needToMove == false)
            {
                Brake(StopSpeed);
            }

            if (_needToMove)
            {
                StartCoroutine(MovingForward(ChargedSpeed));
            }

            if (_itsMovingTime)
            {
                _rigidBody.velocity = transform.forward * LastChargedSpeed;
            }

            if(_wheels.IsGrounded == false)
            {
                _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, MaxFlySpeed);
            }
        }

        public void SetMaxForce(float value)
        {
            _converter.SetMaxForce(value);
        }
        
        public void SetMaxStopSpeed(float value)
        {
            MaxStopSpeed = value;
        }

        public void SetStopSpeed(float value)
        {
            StopSpeed = value;
        }

        public void MoveForward(float force)
        {
            if (_wheels.IsGrounded)
            {
                _rigidBody.velocity = transform.forward* force;

            }
        }

        public IEnumerator MovingForward(float force)
        {
            LastChargedSpeed = force;
            _needToMove = false;
            float elapsedTime = 0;
            float acceleratingTime = 0.1f;
            _itsMovingTime = true;

            while(_wheels.IsGrounded && elapsedTime < acceleratingTime)
            {
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            _itsMovingTime = false;
        }

        public void Brake(float brakeSpeed)
        {
            Vector3 targetVelocity = _rigidBody.velocity;
            targetVelocity.x = 0;
            targetVelocity.z = 0;
            _rigidBody.velocity = Vector3.MoveTowards(_rigidBody.velocity, targetVelocity, brakeSpeed * Time.deltaTime);
            
            if (_rigidBody.velocity.magnitude < brakeSpeed * Time.deltaTime)
            {
                _rigidBody.velocity = targetVelocity;
            }
        }

        public void Respawn(Transform point)
        {
            Respawn(point.position, point.rotation);
        }

        public void Respawn(Vector3 position, Quaternion rotation)
        {
            StartCoroutine(_carRespawn.Respawn(position, rotation));
            _carController.SetStartRotation();
        }
    }
}
