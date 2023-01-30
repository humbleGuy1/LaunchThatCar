using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour, ILoopedTrap
{
    [SerializeField] private float _x;
    [SerializeField] private float _y;
    [SerializeField] private float _z;

    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public AnimationCurve MotionCurve { get; private set; }

    private void Start()
    {
        StartLoop();
    }

    public void StartLoop()
    {
        transform.DOLocalRotate(new Vector3(_x, _y, _z), Duration, RotateMode.FastBeyond360).
            SetEase(MotionCurve).SetLoops(-1, LoopType.Restart);
    }
}