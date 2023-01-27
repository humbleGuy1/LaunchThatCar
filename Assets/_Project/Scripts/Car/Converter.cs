using UnityEngine;
using System;

namespace Runtime.BaseCar
{
    [Serializable]
    public class Converter
    {
        [field:SerializeField] public float MaxForce { get; private set; }

        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _maxYDelta;

        private float _deltaY;

        private readonly float _minForce;
        private readonly float _minYDelta;


        public void SetMaxForce(float value)
        {
            MaxForce = value;
        }

        public float ConvertYDelta()
        {
            _deltaY = _playerInput.StartPosition.y - _playerInput.EndPosition.y;
            _deltaY = Mathf.Clamp(_deltaY, _minYDelta, _maxYDelta);
            float force = Mathf.Lerp(_minForce, MaxForce, _deltaY/_maxYDelta);
            return force;
        }
    }
}

