using UnityEngine;
using System;

namespace Runtime.BaseCar
{
    [Serializable]
    public class Converter
    {
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minYDelta;
        [SerializeField] private float _maxYDelta;

        public float ConvertYDelta(float yDelta)
        {
            yDelta = Mathf.Clamp(yDelta, _minYDelta, _maxYDelta);
            float force = Mathf.Lerp(_minForce,_maxForce, yDelta/_maxYDelta);
            return force;
        }

        public float ConvertXRotation(float xRotation)
        {
            xRotation = Mathf.Clamp(xRotation, -40, 40);
            return xRotation;
        }
    }
}

