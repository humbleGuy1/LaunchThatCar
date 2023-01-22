using Runtime.BaseCar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBust : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CarMover carMover))
            carMover.MoveForward(200);
    }
}
