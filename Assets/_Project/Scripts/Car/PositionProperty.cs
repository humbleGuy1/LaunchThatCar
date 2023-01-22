using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionProperty : MonoBehaviour
{
    [SerializeField] private Transform _up;
    [SerializeField] private Transform _bottom;
    [SerializeField] private Transform _forward;
    [SerializeField] private Transform _back;

    public bool IsUpsideDown { get; private set; }
    public float TiltForward { get; private set; }
    public float TiltSide { get; private set; }
    public Transform Up => _up;
    public Transform Down => _bottom;

    private void Update()
    {
        var distance = _up.position.y - _bottom.position.y;

        TiltForward = GetTiltAngle(transform.localEulerAngles.x);

        TiltSide = GetTiltAngle(transform.eulerAngles.z);
        
        IsUpsideDown = distance < 0.2f;
    }

    private float GetTiltAngle(float eulerAngle)
    {
        float Rotation = eulerAngle;

        if (eulerAngle > 180f)
            Rotation -= 360f;

        return Rotation;
    }

    private void Disable()
    {
        enabled = false;
    }
}
