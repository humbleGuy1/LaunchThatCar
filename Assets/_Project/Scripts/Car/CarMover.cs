using UnityEngine;

namespace Runtime.BaseCar
{
    public class CarMover : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Converter _converter;

        private float _xRotation;
        private float _relaxTime=1;
        public float Force { get; private set; }
        public float MaxForce =>_converter.MaxForce;

        private void Update()
        {

            if (_playerInput.IsButtonUp)
            {
                MoveForward(Force);
            }

            if(_playerInput.IsButtonHold)
            {
                Force= _converter.ConvertYDelta(_playerInput.DeltaY);
                _xRotation = _converter.ConvertXRotation(_playerInput.XRotation);
                Rotate(_xRotation);
            }

            if (_playerInput.IsButtonHold == false)
            {
                float speed = MaxForce / _relaxTime;
                Force =Mathf.MoveTowards(Force, 0, speed*Time.deltaTime);
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

