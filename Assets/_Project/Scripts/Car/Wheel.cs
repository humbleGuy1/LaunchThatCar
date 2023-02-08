using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelCollider _wheelCollider;
    [SerializeField] private GameObject _wheelView;

    public bool DisableWheelColliderControll;

    public float MaxVelocity { get; private set; }
    public float MaxAngularDrag { get; private set; }
    public float MaxFlySpeed { get; private set; }
    public float MaxDrag { get; private set; }
    public float MaxGrip { get; private set; }

    public bool IsGrounded;

    private void Update()
    {
        if (DisableWheelColliderControll)
            return;

        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion qat);
        _wheelView.transform.rotation = qat;
        _wheelView.transform.position = pos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Ground ground))
            IsGrounded = false;
    }

    public void Steer(float steerAngle)
    {
        _wheelCollider.steerAngle = steerAngle;
    }

    public void SetMaxVelocity(GroundProperty groundProperty)
    {
        MaxVelocity = groundProperty.MaxVelocity;
        MaxAngularDrag = groundProperty.MaxAngularDrag;
        MaxFlySpeed = groundProperty.MaxFlySpeed;
        MaxDrag = groundProperty.MaxDrag;
        MaxGrip = groundProperty.MaxGrip;
    }

    public void StartMotor()
    {
    }

    public void Stop()
    {
        _wheelCollider.brakeTorque = 1000f;
    }

    public void Resume(float torque)
    {
        _wheelCollider.brakeTorque = 0;
        _wheelCollider.motorTorque = torque;
    }
}
