using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelCollider _wheelCollider;
    [SerializeField] private GameObject _wheelView;

    public bool IsGrounded => _wheelCollider.isGrounded;

    private void Update()
    {
        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion qat);
        _wheelView.transform.rotation = qat;
        _wheelView.transform.position = pos;
    }
}
