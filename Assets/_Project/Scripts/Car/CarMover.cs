using UnityEngine;

namespace Runtime.BaseCar
{
    public class CarMover : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Converter _converter;

        private float _xRotation;

        private void Update()
        {
            if (_playerInput.IsButtonUp)
            {
               float force = _converter.ConvertYDelta(_playerInput.DeltaY);
                MoveForward(force);
            }

            if(_playerInput.IsButtonHold)
            {
                _xRotation = _converter.ConvertXRotation(_playerInput.XRotation);
                Rotate(_xRotation);
            }
        }

        public void MoveForward(float force)
        {
            _rigidBody.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        public void Rotate(float xRotation)
        {
            transform.rotation = Quaternion.Euler(0, xRotation, 0);
        }
    }
}

