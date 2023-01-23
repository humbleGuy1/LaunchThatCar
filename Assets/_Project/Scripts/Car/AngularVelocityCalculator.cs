using System.Numerics;
using UnityEngine;

public class AngularVelocityCalculator
{
    private float _maxAdditionalAngularDrag = 5f;
    private float _decreaseAdditionalAngularDragTime = 1f;
    private float _maxAngularDrag;
    private float _minAngularDrag;
    private AnimationCurve _interpolator;
    private Rigidbody _rigidBody;
    private WheelsHandler _wheelStatus;
    private float _previousDownVeloicty;
    private float _aditionalAngularDrag;


    public AngularVelocityCalculator(float minAngularDrag, float maxAngularDrag, Rigidbody rigidbody, AnimationCurve angularDragCurve, WheelsHandler wheelsStatus)
    {
        _maxAngularDrag = maxAngularDrag;
        _minAngularDrag = minAngularDrag;
        _interpolator = angularDragCurve;
        _rigidBody = rigidbody;
        _wheelStatus = wheelsStatus;
    }

    public void Update(float maxAngularDrag)
    {
        _maxAngularDrag = maxAngularDrag;

        float currentDownVelocity = _rigidBody.velocity.y;

        if (_previousDownVeloicty < -0.2f)
        {
            float velocityDiffrence = Mathf.Abs(currentDownVelocity - _previousDownVeloicty);

            if (velocityDiffrence > 5f)
                _aditionalAngularDrag = _maxAdditionalAngularDrag;
        }

        _aditionalAngularDrag = Mathf.MoveTowards(_aditionalAngularDrag, 0, _maxAdditionalAngularDrag / _decreaseAdditionalAngularDragTime * Time.deltaTime);

        _previousDownVeloicty = _rigidBody.velocity.y;
    }

    public float Calculate(float currentVelocity, float maxVelocity)
    {
        currentVelocity = Mathf.Clamp(currentVelocity, 0, maxVelocity);
        float angularDragByWheel = _interpolator.Evaluate(_wheelStatus.GroundedWheelCount);
        float angularDragBySpeed = Mathf.Lerp(_minAngularDrag, _maxAngularDrag, currentVelocity/ maxVelocity);
        float newAngularVelocity = angularDragBySpeed*angularDragByWheel +_aditionalAngularDrag;
        
        return newAngularVelocity;
    }
}
