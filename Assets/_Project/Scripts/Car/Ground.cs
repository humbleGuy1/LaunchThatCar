using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _maxAngularDrag;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Wheel wheel))
            wheel.SetMaxVelocity(_maxVelocity, _maxAngularDrag);
    }
}
