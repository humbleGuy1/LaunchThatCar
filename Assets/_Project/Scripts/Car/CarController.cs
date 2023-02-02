using Dreamteck;
using Runtime.BaseCar;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField, Range(0, 10f)] private float _rotationSensitivity;
    [SerializeField, Range(0, 1f)] private float _rotationLimiter;

    private float _xRotation;

    public float RotationSensitivity => _rotationSensitivity;

    private void Awake()
    {
        SetStartRotation();
    }

    public void SetStartRotation()
    {
        _xRotation = GetAngle(transform.eulerAngles.y);
    }

    public void SetSensititvity(float sens)
    {
        _rotationSensitivity = sens;
    }

    public void Rotate(PlayerInput playerInput, float speed, float maxSpeed)
    {
        float calculatedSensitivity = Mathf.Lerp(_rotationSensitivity, 0.5f, speed / maxSpeed);
        _xRotation += playerInput.XRotation * calculatedSensitivity;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, _xRotation, transform.eulerAngles.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
    }

    private float GetAngle(float eulerAngle)
    {
        float Rotation = eulerAngle;

        if (eulerAngle > 180f)
            Rotation -= 360f;

        return Rotation;
    }
}
