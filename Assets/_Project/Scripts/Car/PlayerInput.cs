using UnityEngine;

namespace Runtime.BaseCar
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private const string MouseX = "Mouse X";

        public bool IsButtonUp { get; private set; }
        public bool IsButtonHold { get; private set; }
        public float DeltaY { get; private set; }
        public float XRotation { get; private set; }

        private void Update()
        {
            IsButtonUp = false;

            if (Input.GetMouseButtonDown(0))
            {
                _startPoint = Input.mousePosition;
                IsButtonHold = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                XRotation = 0;
                IsButtonUp = true;
                IsButtonHold = false;
            }

            if (IsButtonHold)
            {
                XRotation = Input.GetAxis(MouseX);
                _endPoint = Input.mousePosition;
                DeltaY = _startPoint.y - _endPoint.y;
            }

        }
    }
}

