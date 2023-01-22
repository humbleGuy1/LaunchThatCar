using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelCollider _wheelCollider;
    [SerializeField] private GameObject _wheelView;

    public float MaxVelocity { get; private set; }
    public float MaxAngularDrag { get; private set; }

    public bool IsGrounded => _wheelCollider.isGrounded;

    private void Update()
    {
        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion qat);
        _wheelView.transform.rotation = qat;
        _wheelView.transform.position = pos;
    }

    public void SetMaxVelocity(float maxSpeed, float maxAngularDrag)
    {
        MaxVelocity = maxSpeed;
        MaxAngularDrag = maxAngularDrag;
    }
}
