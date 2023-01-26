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
        [SerializeField] private float _tenisonForce;

        [field:SerializeField] public float MaxFlySpeed { get; private set; } = 50f;
        [field: SerializeField] public float StopSpeed { get; private set; } = 200f;
        [field: SerializeField] public float MaxStopSpeed { get; private set; } = 75f;

        private AngularVelocityCalculator _angularDragCalculator;
        private CarRespawn _carRespawn;
        private bool _needToMove;
        private bool _itsMovingTime;

        private readonly float _relaxTime = 0.5f;

        public float AcceleratingTime { get; private set; } = 0.1f;
        public float Speed { get; private set; }
        public float RBVelocity => _rigidBody.velocity.magnitude;
        public float MaxSpeed => _converter.MaxForce;
        public bool IsMoving => _rigidBody.velocity.magnitude>0.2f;

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

            _angularDragCalculator.Update(_wheels.MaxAngularDrag);
            _wheels.Update();
            _rigidBody.centerOfMass = _centerOfMassPosition.GetCenterOfMassPosition(_wheels.IsGrounded);
            _rigidBody.angularDrag = _angularDragCalculator.Calculate(_rigidBody.velocity.magnitude, MaxSpeed);

            if(_wheels.IsGrounded == false)
            {
                _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, MaxFlySpeed);
            }

            if (_playerInput.IsButtonDown)
            {
                _carController.SetStartRotation(transform.rotation.y);
            }

            if (_playerInput.IsButtonUp && IsMoving == false)
            {
                _needToMove = true;
            }

            if (_playerInput.IsButtonHold && IsMoving == false)
            {
                Speed = _converter.ConvertYDelta(_playerInput.DeltaY);
                _carController.Rotate(_playerInput);
                Pull();
            }

            if (IsMoving)
            {
                float speed = MaxSpeed / _relaxTime;
                Speed = Mathf.MoveTowards(Speed, 0, speed * Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (_playerInput.IsButtonHold && _wheels.IsGrounded && _rigidBody.velocity.magnitude < MaxStopSpeed &&_needToMove == false)
            {
                Brake(StopSpeed);
            }

            if (_needToMove)
            {
                StartCoroutine(MovingForward(Speed));
                if (_itsMovingTime)
                {
                    _rigidBody.velocity = transform.forward * Speed;

                }
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

        public void SetMaxFlySpeed(float value)
        {
            MaxFlySpeed = value;
            MaxFlySpeed = Mathf.Clamp(MaxFlySpeed, 0, MaxSpeed);
        }

        public void MoveForward(float force)
        {
            if (_wheels.IsGrounded)
            {
                //_needToMove = true;
                _rigidBody.velocity = transform.forward* force;
            }
        }

        public IEnumerator MovingForward(float force)
        {
            _needToMove = false;
            float elapsedTime = 0;
            float acceleratingTime = 3f;
            _itsMovingTime = true;

            while(_wheels.IsGrounded && elapsedTime < acceleratingTime)
            {
                elapsedTime += Time.deltaTime;
                //_rigidBody.MovePosition(_positionProperty.GroundPoint);

                yield return null;
            }

            _itsMovingTime = false;
        }

        public void Brake(float brakeSpeed)
        {
            _rigidBody.velocity = Vector3.MoveTowards(_rigidBody.velocity, Vector3.zero, brakeSpeed * Time.deltaTime);

            if (_rigidBody.velocity.magnitude < brakeSpeed * Time.deltaTime)
            {
                _rigidBody.velocity = Vector3.zero;
            }
        }

        public void Respawn(Transform point)
        {
            StartCoroutine(_carRespawn.Respawn(point));
        }


        private void Pull()
        {
            //if (_playerInput.YRotation < 0 && Speed < MaxSpeed)
            //{
            //    _rigidBody.velocity = -transform.forward * _tenisonForce * (1f - Speed / MaxSpeed);
            //}

            //if (_playerInput.YRotation > 0 && Speed > 0)
            //{
            //    _rigidBody.velocity = -transform.forward * _tenisonForce * (1f - Speed / MaxSpeed);
            //}

            //if(_playerInput.YRotation == 0)
            //{
            //    _rigidBody.velocity = Vector3.zero;
            //}
        }
    }
}
