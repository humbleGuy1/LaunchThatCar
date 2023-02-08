using UnityEngine;

public class AddFuelTrigger : Interactable
{
    [SerializeField] private int _value;

    public override void OnInteract(Car car)
    {
        car.FuelTank.Add(_value);
    }
}
