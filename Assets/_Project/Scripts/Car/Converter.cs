using UnityEngine;
using System;

namespace Runtime.BaseCar
{
    [Serializable]
    public class Converter
    {
        [field:SerializeField] public float MaxForce { get; private set; }

        [SerializeField] private float _maxYDelta;

        private readonly float _minForce;
        private readonly float _minYDelta;

        public float ConvertYDelta(float yDelta)
        {
            yDelta = Mathf.Clamp(yDelta, _minYDelta, _maxYDelta);
            float force = Mathf.Lerp(_minForce,MaxForce, yDelta/_maxYDelta);
            return force;
        }

        public float ConvertXRotation(float xRotation)
        {
            xRotation = Mathf.Clamp(xRotation, -40, 40);
            return xRotation;
        }
    }
}

