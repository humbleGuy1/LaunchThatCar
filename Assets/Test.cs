using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float _strength;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _wheel;
    [SerializeField] private GameObject _wheel2;
    [SerializeField] private WheelCollider _wheelCollider;
    [SerializeField] private WheelCollider _wheelCollider2;
    [SerializeField] private WheelCollider _wheelCollider3;
    [SerializeField] private WheelCollider _wheelCollider4;

    private void Awake()
    {

        _wheelCollider.attachedRigidbody.maxAngularVelocity = Mathf.Infinity;
        _wheelCollider2.attachedRigidbody.maxAngularVelocity = Mathf.Infinity;
        _wheelCollider3.attachedRigidbody.maxAngularVelocity = Mathf.Infinity;
        _wheelCollider4.attachedRigidbody.maxAngularVelocity = Mathf.Infinity;
    }

    public void Kick()
    {
        Debug.Log(_wheelCollider.attachedRigidbody.maxAngularVelocity);
        _wheelCollider.motorTorque = _strength;
        _wheelCollider2.motorTorque = _strength;
        _rigidbody.AddForce(transform.forward*1000, ForceMode.Acceleration);
    }

    private void Update()
    {
        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion qat);
        _wheel.transform.rotation = qat;
        _wheelCollider2.GetWorldPose(out Vector3 pos2, out Quaternion qat2);
        _wheel2.transform.rotation = qat2;
    }
}
