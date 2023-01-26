using System;
using UnityEngine;

[Serializable]
public class GroundProperty
{
    [field: SerializeField] public float MaxVelocity {get;private set;}
    [field: SerializeField] public float MaxAngularDrag { get; private set; }
    [field: SerializeField] public float MaxFlySpeed { get; private set; }

    public void SetProperty(float maxVelocity, float maxAngularDrag)
    {
        MaxVelocity = maxVelocity;
        MaxAngularDrag = maxAngularDrag;
    }
}
