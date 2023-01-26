using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionProperty : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _up;
    [SerializeField] private Transform _bottom;
    [SerializeField] private Transform _forward;
    [SerializeField] private Transform _back;

    public Vector3 Forward { get; private set; }
    public Vector3 GroundPoint { get; private set; }
    public Quaternion ForwardAlongSurface { get; private set; }
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

        UpdateForwardAlongSuface();
    }

    private float GetTiltAngle(float eulerAngle)
    {
        float Rotation = eulerAngle;

        if (eulerAngle > 180f)
            Rotation -= 360f;

        return Rotation;
    }

    private void UpdateForwardAlongSuface()
    {
        if(Physics.Raycast(_up.position, Vector3.down, out RaycastHit hit,10f, _layerMask))
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            Vector3 normal = hit.normal;
            GroundPoint = hit.point;
            ForwardAlongSurface = Quaternion.LookRotation(forward, normal);
        }
    }

    private void Disable()
    {
        enabled = false;
    }
}
