using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class WheelsHandler
{
    [SerializeField] private Wheel[] _wheels;
    [SerializeField] private Wheel[] _steeringWheel;

    public float MaxGrip { get; private set; }
    public float MaxFlySpeed { get; private set; }
    public float AverageMaxVelocity { get; private set; }
    public float MaxAngularDrag { get; private set; }
    public int GroundedWheelCount { get; private set; }
    public int MaxWheelCount => _wheels.Length;
    public bool IsGearWheelGrounded => _wheels.Any(wheel => wheel.IsGrounded);
    public bool IsSteerWheelGrounded => _steeringWheel.Any(wheel => wheel.IsGrounded);
    public bool BecomeGrounded { get; private set; }

    public void Update()
    {
        GroundedWheelCount = 0;

        foreach (var wheel in _wheels)
        {
            if(wheel.IsGrounded)
                GroundedWheelCount++;
        }

        AverageMaxVelocity = GetMaxVelocityByGround();
        MaxAngularDrag = GetMaxAngularDrag();
        MaxFlySpeed = GetMaxFlySpeed();
        MaxGrip = GetAverageGrip();
    }

    public void Steer(float angle)
    {
        foreach (var steerWheel in _steeringWheel)
        {
            steerWheel.Steer(angle);
        }
    }

    public void Stop()
    {
        foreach (var wheel in _wheels)
        {
            wheel.Stop();
        }
    }

    public void Resume(float torque)
    {
        foreach (var wheel in _wheels)
        {
            wheel.Resume(torque);
        }
    }

    public void DisableWheelColiderControll(bool disable)
    {
        foreach (var wheel in _wheels)
        {
            wheel.DisableWheelColliderControll = disable;
        }
    }

    private float GetAverageGrip()
    {
        float value = 0;

        foreach (var wheel in _wheels)
        {
            value += wheel.MaxGrip;
        }

        return value / _wheels.Length;
    }

    private float GetMaxFlySpeed()
    {
        float value = 0;

        foreach (var wheel in _wheels)
        {
            value += wheel.MaxFlySpeed;
        }

        return value / _wheels.Length;
    }

    private float GetMaxAngularDrag()
    {
        float value = 0;

        foreach (var wheel in _wheels)
        {
            value += wheel.MaxAngularDrag;
        }
        
        return value / _wheels.Length ;
    }

    private float GetMaxVelocityByGround()
    {
        float maxAngularDrag =0;

        foreach (var wheel in _wheels)
        {
            maxAngularDrag += wheel.MaxVelocity;
        }

        return maxAngularDrag / _wheels.Length;
    }
}
