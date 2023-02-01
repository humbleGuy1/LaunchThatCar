using System;
using UnityEngine;

[Serializable]
public class GroundProperty
{
    [field: SerializeField] public float MaxVelocity {get;private set;}
    [field: SerializeField] public float MaxAngularDrag { get; private set; } = 5f;
    [field: SerializeField] public float MaxFlySpeed { get; private set; } = 50f;
    [field: SerializeField] public float MaxDrag { get; private set; }
    [field: SerializeField] public float MaxGrip { get; private set; } = 100f;

    public void SetProperty(float maxVelocity, float maxAngularDrag)
    {
        MaxVelocity = maxVelocity;
        MaxAngularDrag = maxAngularDrag;
    }
}
