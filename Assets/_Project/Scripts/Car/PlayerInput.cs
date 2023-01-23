using UnityEngine;

namespace Runtime.BaseCar
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";

        public bool IsButtonUp => Input.GetMouseButtonUp(0);
        public bool IsButtonDown => Input.GetMouseButtonDown(0);
        public bool SpacePressed => Input.GetKey(KeyCode.Space);
        public bool IsButtonHold { get; private set; }
        public float DeltaY { get; private set; }
        public float XRotation { get; private set; }
        public float YRotation { get; private set; }

        private void Update()
        {
            if (IsButtonDown)
            {
                _startPoint = Input.mousePosition;
                IsButtonHold = true;
            }

            if (IsButtonUp)
            {
                XRotation = 0;
                IsButtonHold = false;
            }

            if (IsButtonHold)
            {
                XRotation = Input.GetAxis(MouseX);
                YRotation = Input.GetAxis(MouseY);
                _endPoint = Input.mousePosition;
                DeltaY = _startPoint.y - _endPoint.y;
            }
        }
    }
}

