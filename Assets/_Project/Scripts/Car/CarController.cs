using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField, Range(0, 4f)] private float _rotationSensitivity;
    [SerializeField, Range(0, 1f)] private float _rotationLimiter;

    private float _currentRotation;
    private float _startRotation;
    private float _deltaRotation;

    public void SetStartRotation(float startRotation)
    {
        _startRotation = startRotation;
    }

    public void SetSensititvity(float sens)
    {
        _rotationSensitivity = sens;
    }

    public void Rotate(float xRotation)
    {
        Quaternion savedRotation = transform.rotation;
        transform.Rotate((Vector3.up * xRotation).normalized * _rotationSensitivity);
        _currentRotation = transform.rotation.y;
        _deltaRotation = _startRotation - _currentRotation;

        if (Mathf.Abs(_deltaRotation) >= _rotationLimiter)
            transform.rotation = savedRotation;
    }
}
