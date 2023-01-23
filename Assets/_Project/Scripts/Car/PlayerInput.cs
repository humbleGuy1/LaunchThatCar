using UnityEngine;

namespace Runtime.BaseCar
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";

        public bool SpacePressed { get; private set; }
        public bool IsButtonUp { get; private set; }
        public bool IsButtonDown { get; private set; }
        public bool IsButtonHold { get; private set; }
        public float DeltaY { get; private set; }
        public float XRotation { get; private set; }
        public float YRotation { get; private set; }

        private void Update()
        {
            SpacePressed = false;
            IsButtonUp = false;
            IsButtonDown = false;

            if (Input.GetMouseButtonDown(0))
            {
                _startPoint = Input.mousePosition;
                IsButtonHold = true;
                IsButtonDown = true;
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
                YRotation = Input.GetAxis(MouseY);
                _endPoint = Input.mousePosition;
                DeltaY = _startPoint.y - _endPoint.y;
            }

            if (Input.GetKey(KeyCode.Space))
                SpacePressed = true;

        }
    }
}

