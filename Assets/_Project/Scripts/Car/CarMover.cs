using System.Linq;
using UnityEngine;

namespace Runtime.BaseCar
{
    public class CarMover : MonoBehaviour, IRespawnable
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Converter _converter;
        [SerializeField] private WheelsStatus _wheelStatus;
        [SerializeField] private CenterOfMassPosition _centerOfMassPosition = new CenterOfMassPosition();
        [SerializeField] private PositionProperty _positionProperty;
        [SerializeField] private AnimationCurve _angularDragCurve;
        [SerializeField] private CarController _carController;

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
                _carController.SetStartRotation(transform.rotation.y);
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

        public void MoveForward(float force)
        {
            if(_wheelStatus.IsGrounded)
                _rigidBody.velocity = transform.forward * force;
        }

        public void Respawn(Transform point)
        {
            StartCoroutine(_carRespawn.Respawn(point));
        }

        private void Pull()
        {
            if (_playerInput.YRotation < 0 && Speed < MaxSpeed)
            {
                _rigidBody.velocity = -transform.forward * 3f;
            }

            if (_playerInput.YRotation > 0 && Speed > 0)
            {
                _rigidBody.velocity = transform.forward * 3f;
            }

            if (_playerInput.YRotation == 0)
            {
                _rigidBody.velocity = Vector3.zero;
            }
        }
    }
}
