using Runtime.BaseCar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
            OnInteract(car);
    }

    public abstract void OnInteract(Car car);
}
