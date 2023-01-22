using System.Linq;
using UnityEngine;

namespace Runtime.BaseCar
{
    public class CarMover : MonoBehaviour, IRespawnable
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Converter _converter;
        [SerializeField, Min(0)] private float _rotationSensitivity;
        [SerializeField, Range(0, 1f)] private float _rotationLimiter;
        [SerializeField] private WheelsStatus _wheelStatus;
        [SerializeField] private CenterOfMassPosition _centerOfMassPosition = new CenterOfMassPosition();
        [SerializeField] private PositionProperty _positionProperty;
        [SerializeField] private AnimationCurve _angularDragCurve;

        private float _startRotation;
        private float _currentRotation;
        private float _deltaRotation;
        private AngularVelocityCalculator _angularDragCalculator;
        private CarRespawn _carRespawn;

        private readonly float _relaxTime = 1;
        public float Speed { get; private set; }
        public float MaxSpeed => _converter.MaxForce;

        private void Awake()
        {
            _carRespawn = new CarRespawn(_rigidBody, transform);
            _angularDragCalculator = new AngularVelocityCalculator(0, 3,_rigidBody, _angularDragCurve, _wheelStatus);
            _centerOfMassPosition.Init(_positionProperty);
        }

        private void Update()
        {
            _angularDragCalculator.Update(_wheelStatus.MaxAngularDrag);
            _wheelStatus.Update();
            _rigidBody.centerOfMass = _centerOfMassPosition.GetCenterOfMassPosition(_wheelStatus.IsGrounded);
            _rigidBody.angularDrag = _angularDragCalculator.Calculate(_rigidBody.velocity.magnitude, MaxSpeed);

            if (_playerInput.IsButtonUp)
            {
                MoveForward(Speed);
                _startRotation = transform.rotation.y;
            }

            if(_playerInput.IsButtonHold)
            {
                Speed = _converter.ConvertYDelta(_playerInput.DeltaY);
                Rotate(_playerInput.XRotation);
            }

            if (_playerInput.IsButtonHold == false)
            {
                float speed = MaxSpeed / _relaxTime;
                Speed = Mathf.MoveTowards(Speed, 0, speed * Time.deltaTime);
            }
        }

        public void MoveForward(float force)
        {
            if(_wheelStatus.IsGrounded)
                _rigidBody.velocity = transform.forward * force;
        }

        public void Rotate(float xRotation)
        {
            Quaternion savedRotation = transform.rotation;
            transform.Rotate((Vector3.up * xRotation).normalized * _rotationSensitivity);
            _currentRotation = transform.rotation.y;
            _deltaRotation = _startRotation - _currentRotation;

            if (Mathf.Abs(_deltaRotation) >= _rotationLimiter)
                transform.rotation = savedRotation;
        }

        public void Respawn(Transform point)
        {
            StartCoroutine(_carRespawn.Respawn(point));
        }
    }
}
