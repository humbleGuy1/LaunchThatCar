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

        public bool IsButtonUp { get; private set;}
        public bool IsButtonDown {get; private set;}
        public bool SpacePressed {get; private set;}
        public bool IsButtonHold {get; private set;}
        public float DeltaY { get; private set; }
        public float XRotation { get; private set; }
        public float xXxRotationxXx { get; private set; }
        public float YRotation { get; private set; }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            IsButtonUp = Input.GetMouseButtonUp(0);
            IsButtonDown = Input.GetMouseButtonDown(0);
            SpacePressed = Input.GetKeyDown(KeyCode.Space);
            IsButtonHold = Input.GetMouseButton(0);

            if (Input.GetMouseButtonDown(0))
            {
                _startPoint = Input.mousePosition;
            }

            if (IsButtonUp)
            {
                XRotation = 0;
                xXxRotationxXx = 0;
            }

            if (IsButtonHold)
            {
                XRotation = Input.GetAxis(MouseX);
                xXxRotationxXx += Input.GetAxis(MouseX);
                YRotation = Input.GetAxis(MouseY);

                _endPoint = Input.mousePosition;
                DeltaY = _startPoint.y - _endPoint.y;
            } 
        }
    }
}

