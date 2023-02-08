using UnityEngine;
using Runtime.BaseCar;

[SelectionBase]
public class Car : MonoBehaviour
{
    [SerializeField] private CarMover _carMover;
    [SerializeField] private FuelTank _fuelTank;

    public CarMover CarMover => _carMover;
    public FuelTank FuelTank => _fuelTank;

    public void AttachToPlatform(AttachableObject platform)
    {
        transform.SetParent(platform.transform);
    }

    public void DetachFromPlatform()
    {
        transform.SetParent(null);
    }
}



