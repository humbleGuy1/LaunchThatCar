using UnityEngine;

namespace Runtime.BaseCar
{
    [SelectionBase]
    public class Car : MonoBehaviour
    {
        [SerializeField] private CarMover _carMover;
        [SerializeField] private PositionProperty _positionProperty;

        public CarMover CarMover => _carMover;
        public PositionProperty PositionProperty => _positionProperty;

        public void AttachToPlatform(AttachableObject platform)
        {
            transform.SetParent(platform.transform);
        }

        public void DetachFromPlatform()
        {
            transform.SetParent(null);
        }
    }
}

