using UnityEngine;

public interface IInput
{
    public Vector3 StartPosition { get; }
    public Vector3 EndPosition { get; }

    public bool IsButtonUp { get; }
    public bool IsButtonDown { get; }
    public bool SpacePressed { get; }
    public bool IsButtonHold { get; }
    public float XRotation { get; }
}

