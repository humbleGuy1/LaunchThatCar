using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.BaseCar
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector3 StartPosition { get; private set; }
        public Vector3 EndPosition { get; private set; }

        private const string MouseX = "Mouse X";

        public bool IsButtonUp { get; private set;}
        public bool IsButtonDown {get; private set;}
        public bool SpacePressed {get; private set;}
        public bool IsButtonHold {get; private set;}
        public float XRotation { get; private set; }
        public float xXxRotationxXx { get; private set; }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            IsButtonUp = Input.GetMouseButtonUp(0);
            IsButtonDown = Input.GetMouseButtonDown(0);
            SpacePressed = Input.GetKeyDown(KeyCode.Space);
            IsButtonHold = Input.GetMouseButton(0);

            if (IsButtonDown)
            {
                StartPosition = Input.mousePosition;
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
                EndPosition = Input.mousePosition;
            } 
        }
    }
}

