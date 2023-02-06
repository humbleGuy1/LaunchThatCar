using Runtime.BaseCar;
using UnityEngine;

public class SpeedBust : Interactable
{
    [SerializeField] private float _speed = 200;

    public override void OnInteract(Car car)
    {
        car.CarMover.MoveForward(_speed);
    }
}
