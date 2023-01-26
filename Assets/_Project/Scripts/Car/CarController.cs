using Dreamteck;
using Runtime.BaseCar;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField, Range(0, 10f)] private float _rotationSensitivity;
    [SerializeField, Range(0, 1f)] private float _rotationLimiter;

    private float _xRotation;
    private bool _isRotationInverted;
    private bool _isNewRotationType;

    public float RotationSensitivity => _rotationSensitivity;

    private void Awake()
    {
        _xRotation = GetAngle(transform.eulerAngles.y);
    }

    public void SetStartRotation()
    {
        _xRotation = GetAngle(transform.eulerAngles.y);
    }

    public void SetSensititvity(float sens)
    {
        _rotationSensitivity = sens;
    }

    public void SetInvertion(bool toglePressed)
    {
        _isRotationInverted = toglePressed;
    }

    public void SetRotationType(bool toglePressed)
    {
        _isNewRotationType = toglePressed;
    }

    public void Rotate(PlayerInput playerInput, float speed, float maxSpeed)
    {
        float calculatedSensitivity = Mathf.Lerp(_rotationSensitivity, 1, speed / maxSpeed);

        if (_isNewRotationType == false)
        {
            if (_isRotationInverted)
                _xRotation += playerInput.XRotation * calculatedSensitivity * -1;
            else
                _xRotation += playerInput.XRotation * calculatedSensitivity;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, _xRotation, transform.eulerAngles.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
        else
        {
            if (_isRotationInverted)
                transform.Rotate((Vector3.up * playerInput.xXxRotationxXx).normalized * _rotationSensitivity * -1);
            else
                transform.Rotate((Vector3.up * playerInput.xXxRotationxXx).normalized * _rotationSensitivity);
        }
       
    }

    private float GetAngle(float eulerAngle)
    {
        float Rotation = eulerAngle;

        if (eulerAngle > 180f)
            Rotation -= 360f;

        return Rotation;
    }
}
