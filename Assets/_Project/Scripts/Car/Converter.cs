using UnityEngine;
using System;

namespace Runtime.BaseCar
{
    [Serializable]
    public class Converter
    {
        [field:SerializeField] public float MaxForce { get; private set; }

        [SerializeField] private float _maxYDelta;

        private float _deltaY;
        private IInput _input;

        private readonly float _minForce;
        private readonly float _minYDelta;

        public void Init(IInput playerInput)
        {
            _input = playerInput;
        }

        public void SetMaxForce(float value)
        {
            MaxForce = value;
        }

        public float ConvertYDelta()
        {
            _deltaY = _input.StartPosition.y - _input.EndPosition.y;

            return ConvertYDelta(_deltaY);
        }

        public float ConvertYDelta(float delta)
        {
            delta = Mathf.Clamp(delta, _minYDelta, _maxYDelta);
            float force = Mathf.Lerp(_minForce, MaxForce, delta / _maxYDelta);

            return force;
        }
    }
}

