using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Ground))]
public class Ramp : MonoBehaviour
{
    [SerializeField] private Ground _ground;
    [SerializeField] private RampType _rampType;
    [SerializeField] private RampsTypes _types;

    public RampProperty CurrentProperty { get; private set; }

    private void Start()
    {
        SetProperty(_types.GetPropertyByType(_rampType));
    }

    public void SetProperty(int rampType)
    {
        SetProperty(_types.GetPropertyByType((RampType)rampType));
    }

    public void SetProperty(RampProperty rampProperty)
    {
        CurrentProperty = rampProperty;
        _ground.Property.SetProperty(rampProperty.MaxSpeed, rampProperty.MaxAngularDrag);
        SetRotation(rampProperty.XAngle);
        SetDistance(rampProperty.Distance);
        //SetWidth(rampProperty.Width);
    }

    public void SetRotation(float xAngle)
    {
        Quaternion rotation = Quaternion.Euler(xAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = rotation;
    }

    public void SetDistance(float distance)
    {
        Vector3 scale = new Vector3(transform.localScale.x, distance, transform.localScale.z);
        transform.localScale = scale;
    }

    public void SetWidth(float width)
    {
        Vector3 scale = new Vector3(width, transform.localScale.y, transform.localScale.z);
        transform.localScale = scale;
    }
}
