using UnityEngine;
using UnityEngine.EventSystems;

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
        public bool SpacePressed => Input.GetKeyDown(KeyCode.Space);
        public bool IsButtonHold => Input.GetMouseButton(0);
        public float DeltaY { get; private set; }
        public float XRotation { get; private set; }
        public float YRotation { get; private set; }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _startPoint = Input.mousePosition;
            }

            if (IsButtonUp)
            {
                XRotation = 0;
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

