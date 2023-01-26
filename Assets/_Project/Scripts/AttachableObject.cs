using Runtime.BaseCar;
using UnityEngine;

public class AttachableObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            car.AttachToPlatform(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            car.DetachFromPlatform();
        }
    }
}
