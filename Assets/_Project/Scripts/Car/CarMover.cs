using System.Linq;
using UnityEngine;

namespace Runtime.BaseCar
{
    public class CarMover : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Converter _converter;
        [SerializeField, Min(0)] private float _rotationSensitivity;
        [SerializeField] private Transform _massCenter;
        [SerializeField, Range(0, 1f)] private float _rotationLimiter;
        [SerializeField] private Wheel[] _wheel;

        private float _startRotation;
        private float _currentRotation;
        private float _deltaRotation;
        private float _elapsedTime;

        private readonly float _relaxTime = 1;
        public float Force { get; private set; }
        public float MaxForce => _converter.MaxForce;
        public bool IsGrounded => _wheel.Any(wheel => wheel.IsGrounded);

        private void Awake()
        {
            _rigidBody.centerOfMass = _massCenter.localPosition;
        }

        private void Update()
        {
            _rigidBody.angularDrag = 0;
            Debug.Log(IsGrounded);
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

            if (IsGrounded == false)
            {
                _elapsedTime += Time.deltaTime;
                _rigidBody.angularDrag = 100;
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime*40);
            }

            if (IsGrounded)
            {
                _elapsedTime = 0;
            }
        }

        public void MoveForward(float force)
        {
            //_rigidBody.AddForce(transform.forward * force, ForceMode.VelocityChange);
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
    }
}

