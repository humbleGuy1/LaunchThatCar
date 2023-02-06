using Dreamteck.Splines;
using UnityEngine;

public class PositionProperty : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _up;
    [SerializeField] private Transform _bottom;
    [SerializeField] private Transform _forward;
    [SerializeField] private Transform _back;
    [SerializeField] private Rigidbody _rb;
    [Header("Spline")]
    [SerializeField] private SplineProjector _splineProjector;
    [SerializeField] private float _maxDegreesDelta;

    [field: SerializeField] public GroundCheck GroundCheck { get; private set; }

    public Vector3 Forward { get; private set; }
    public Vector3 GroundPoint { get; private set; }
    public Quaternion ForwardAlongSurface { get; private set; }
    public bool IsOnCarcase { get; private set; }
    public float TiltForwardAngle { get; private set; }
    public float TiltSideAngle { get; private set; }
    public Transform Up => _up;
    public Transform Down => _bottom;

    private void Update()
    {
        TiltForwardAngle = GetTiltAngle(transform.localEulerAngles.x);

        TiltSideAngle = GetTiltAngle(transform.eulerAngles.z);

        IsOnCarcase = GroundCheck.Grounded && (Mathf.Abs(TiltSideAngle) > 85 || Mathf.Abs(TiltForwardAngle) > 85);

        if (_splineProjector.spline != null)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _splineProjector.result.rotation, _maxDegreesDelta * Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(_splineProjector.result.forward, transform.up);
    }

    public void SetSplineToFollow(SplineComputer spline)
    {
        _splineProjector.spline = spline;
    }

    public void ResetSpline()
    {
        _splineProjector.spline = null;
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
        if(Physics.Raycast(_up.position, Vector3.down, out RaycastHit hit,4f, _layerMask))
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
