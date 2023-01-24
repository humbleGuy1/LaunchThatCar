using Dreamteck;
using System.Linq;
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

        private AngularVelocityCalculator _angularDragCalculator;
        private CarRespawn _carRespawn;
        private float _elapsedTime;
        private float _maxDistance = 1;
        private float _currentDistance;
        private bool isConstant;

        private readonly float _relaxTime = 0.5f;

        public float MaxStopSpeed { get; private set; } = 50f;
        public float StopSpeed { get; private set; } = 200f;
        public float MaxFlySpeed { get; private set; } = 50f;
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
                //_rigidBody.angularVelocity *= 1.2f;
            }

            if (_playerInput.IsButtonDown)
            {
                _carController.SetStartRotation(transform.rotation.y);
            }

            if (_playerInput.IsButtonUp && IsMoving == false)
            {
                MoveForward(Speed);
            }            

            if(_playerInput.IsButtonHold && IsMoving == false)
            {
                Speed = _converter.ConvertYDelta(_playerInput.DeltaY);
                _carController.Rotate(_playerInput.XRotation);
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
            if (_playerInput.IsButtonHold && _wheels.IsGrounded && _rigidBody.velocity.magnitude < MaxStopSpeed)
            {
                Brake(StopSpeed);
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
                _rigidBody.velocity = transform.forward * force;
            }
        }

        public void Brake(float brakeSpeed)
        {
            _rigidBody.velocity = Vector3.MoveTowards(_rigidBody.velocity, Vector3.zero, brakeSpeed * Time.deltaTime);
            if (_rigidBody.velocity.magnitude < 10)
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
