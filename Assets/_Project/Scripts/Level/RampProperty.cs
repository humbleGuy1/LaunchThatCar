using System;
using UnityEngine;

[Serializable]
public class RampProperty
{
    [field: SerializeField] public RampType Type { get; private set; }
    [field: SerializeField] public float XAngle { get; private set; }
    [field: SerializeField] public float Distance { get; private set; }
    [field: SerializeField] public float Width { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float MaxAngularDrag { get; private set; }

    public void SetProperty(RampType type, float xAngle, float distance, float width, float maxSpeed, float maxAngularDrag)
    {
        Type = type;
        XAngle = xAngle;
        Distance = distance;
        Width = width;
        MaxSpeed = maxSpeed;
        MaxAngularDrag = maxAngularDrag;
    }
}
