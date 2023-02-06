using Runtime.BaseCar;
using UnityEngine;

public class SpeedBust : MonoBehaviour
{
    [SerializeField] private float _speed = 200;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            car.CarMover.MoveForward(_speed);
            //car.CarMover.CheckPointReached = true;
        }
    }
}
