using UnityEngine;
using Runtime.BaseCar;

[SelectionBase]
public class Car : MonoBehaviour
{
    [SerializeField] private CarMover _carMover;

    public CarMover CarMover => _carMover;

    public void AttachToPlatform(AttachableObject platform)
    {
        transform.SetParent(platform.transform);
    }

    public void DetachFromPlatform()
    {
        transform.SetParent(null);
    }
}



