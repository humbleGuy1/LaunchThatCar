using Dreamteck;
using Runtime.BaseCar;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField, Range(0, 4f)] private float _rotationSensitivity;
    [SerializeField, Range(0, 1f)] private float _rotationLimiter;

    private float _currentRotation;
    private float _startRotation;
    private float _deltaRotation;
    private float _xRotation;

    public float RotationSensitivity => _rotationSensitivity;

    private void Awake()
    {
        _xRotation = GetAngle(transform.eulerAngles.y);
    }

    public void SetStartRotation(float startRotation)
    {
        _startRotation = startRotation;
        _xRotation = GetAngle(transform.eulerAngles.y);
    }

    public void SetSensititvity(float sens)
    {
        _rotationSensitivity = sens;
    }

    public void Rotate(PlayerInput playerInput)
    {
        _xRotation += playerInput.XRotation;
        Quaternion savedRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, _xRotation, transform.eulerAngles.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSensitivity*10);
        _currentRotation = transform.rotation.y;
        _deltaRotation = _startRotation - _currentRotation;

        if (Mathf.Abs(_deltaRotation) >= _rotationLimiter)
            transform.rotation = savedRotation;
    }

    private float GetAngle(float eulerAngle)
    {
        float Rotation = eulerAngle;

        if (eulerAngle > 180f)
            Rotation -= 360f;

        return Rotation;
    }
}
