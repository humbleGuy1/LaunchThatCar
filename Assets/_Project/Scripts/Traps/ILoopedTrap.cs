using UnityEngine;

public interface ILoopedTrap
{
    public float Duration { get; }
    public AnimationCurve MotionCurve { get; }

    public void StartLoop(); 
}
