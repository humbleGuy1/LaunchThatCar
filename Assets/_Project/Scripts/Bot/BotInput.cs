using UnityEngine;

public class BotInput : MonoBehaviour, IInput
{
    public Vector3 StartPosition { get; private set; }
    public Vector3 EndPosition { get; private set; }

    public bool IsButtonUp { get; private set; }
    public bool IsButtonDown { get; private set; }
    public bool SpacePressed { get; private set; }
    public bool IsButtonHold { get; private set; }
    public float XRotation { get; private set; }
}
