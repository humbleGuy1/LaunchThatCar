using UnityEngine;

namespace Runtime.BaseCar
{
    [SelectionBase]
    public class Car : MonoBehaviour
    {
        [SerializeField] private CarMover _carMover;
    }
}

