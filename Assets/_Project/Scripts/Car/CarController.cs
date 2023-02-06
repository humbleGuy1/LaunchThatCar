using Dreamteck;
using Runtime.BaseCar;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField, Range(0, 10f)] private float _rotationSensitivity;
    [SerializeField, Range(0, 1f)] private float _rotationLimiter;

    private float _xRotation;
    private Rigidbody _rigidbody;

    public float RotationSensitivity => _rotationSensitivity;

    private void Start()
    {
        SetStartRotation();
    }

    public void Init(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void SetStartRotation()
    {
        _xRotation = GetAngle(transform.rotation.eulerAngles.y);
    }

    public void SetSensititvity(float sens)
    {
        _rotationSensitivity = sens;
    }

    public void Rotate(IInput input, float speed, float maxSpeed)
    {
        float calculatedSensitivity = Mathf.Lerp(_rotationSensitivity, 0.5f, speed / maxSpeed);
        _xRotation += input.XRotation * calculatedSensitivity;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, _xRotation, transform.rotation.eulerAngles.z));
        Rotate(targetRotation, 10f * Time.fixedDeltaTime);
    }

    public void Rotate(Quaternion targetRotation, float lerp)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerp );
    }

    private float GetAngle(float eulerAngle)
    {
        float Rotation = eulerAngle;

        if (eulerAngle > 180f)
            Rotation -= 360f;

        return Rotation;
    }
}
