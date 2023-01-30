using Runtime.BaseCar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBust : MonoBehaviour
{
    [SerializeField] private float _speed =200;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CarMover carMover))
        {
            carMover.MoveForward(_speed);
            //StartCoroutine(carMover.MovingForward(_speed));
        }
    }
}
