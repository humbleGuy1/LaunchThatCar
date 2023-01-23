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

        private readonly float _relaxTime = 1;
        public float Speed { get; private set; }
        public float MaxSpeed => _converter.MaxForce;

        private void Awake()
        {
            _carRespawn = new CarRespawn(_rigidBody, transform, _wheels);
            _angularDragCalculator = new AngularVelocityCalculator(0, 3,_rigidBody, _angularDragCurve, _wheels);
            _centerOfMassPosition.Init(_positionProperty);
        }

        private void Update()
        {
            _angularDragCalculator.Update(_wheels.MaxAngularDrag);
            _wheels.Update();
            _rigidBody.centerOfMass = _centerOfMassPosition.GetCenterOfMassPosition(_wheels.IsGrounded);
            _rigidBody.angularDrag = _angularDragCalculator.Calculate(_rigidBody.velocity.magnitude, MaxSpeed);

            if(_wheels.IsGrounded == false)
            {
                _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, 50);
                //_rigidBody.angularVelocity *= 1.2f;
            }

            if (_playerInput.IsButtonDown)
            {
                _carController.SetStartRotation(transform.rotation.y);
            }

            if (_playerInput.IsButtonUp)
            {
                MoveForward(Speed);
            }

            if(_playerInput.IsButtonHold)
            {
                Speed = _converter.ConvertYDelta(_playerInput.DeltaY);
                _carController.Rotate(_playerInput.XRotation);
                Pull();
            }

            if (_playerInput.IsButtonHold == false)
            {
                float speed = MaxSpeed / _relaxTime;
                Speed = Mathf.MoveTowards(Speed, 0, speed * Time.deltaTime);
            }
        }

        public void SetMaxForce(float value)
        {
            _converter.SetMaxForce(value);
        }

        public void MoveForward(float force)
        {
            if(_wheels.IsGrounded)
                _rigidBody.velocity = transform.forward * force;
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
                
            //    print(_currentDistance);
            //}
        }
    }
}
