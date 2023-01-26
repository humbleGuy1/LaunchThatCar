using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [field: SerializeField] public GroundProperty Property { get; private set; }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Wheel wheel))
        {
            wheel.SetMaxVelocity(Property);
        }
    }
}
