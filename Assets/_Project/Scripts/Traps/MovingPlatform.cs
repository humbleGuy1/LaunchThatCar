using UnityEngine;
using DG.Tweening;

[SelectionBase]
public class MovingPlatform : MonoBehaviour, ILoopedTrap
{
    [SerializeField] private Transform _endPoint;

    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public AnimationCurve MotionCurve { get; private set; }

    private Vector3 _startValue;

    private void Start()
    {
        _startValue = transform.position;

        StartLoop();
    }

    public void StartLoop()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(_endPoint.position, Duration));
        sequence.Append(transform.DOMove(_startValue, Duration));
        sequence.SetLoops(-1, LoopType.Restart).SetEase(MotionCurve);
    }
}
