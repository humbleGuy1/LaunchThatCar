using UnityEngine;

namespace Runtime.BaseCar
{
    public class CarMover : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Converter _converter;
        [SerializeField] private float _rotationSensitivity;
        [SerializeField, Range(0, 1f)] private float _rotationLimiter;

        private float _startRotation;
        private float _currentRotation;
        private float _deltaRotation;

        private readonly float _relaxTime = 1;
        public float Force { get; private set; }
        public float MaxForce => _converter.MaxForce;
        public bool IsMoving => _rigidBody.velocity != Vector3.zero;

        private void Update()
        {
            if (IsMoving)
                return;

            if (_playerInput.IsButtonUp)
            {
                MoveForward(Force);
                _startRotation = transform.rotation.y;
            }

            if(_playerInput.IsButtonHold)
            {
                Force = _converter.ConvertYDelta(_playerInput.DeltaY);
                Rotate(_playerInput.XRotation);
            }

            if (_playerInput.IsButtonHold == false)
            {
                float speed = MaxForce / _relaxTime;
                Force = Mathf.MoveTowards(Force, 0, speed * Time.deltaTime);
            }
        }

        public void MoveForward(float force)
        {
            _rigidBody.AddForce(transform.forward * force, ForceMode.Impulse);
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
    }
}

