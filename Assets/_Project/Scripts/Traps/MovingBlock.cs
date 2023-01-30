using UnityEngine;
using DG.Tweening;

public class MovingBlock : MonoBehaviour, ILoopedTrap
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _interval;

    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public AnimationCurve MotionCurve { get; private set; }

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;

       StartLoop();
    }

    public void StartLoop()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(_targetPoint.position, Duration));
        sequence.AppendInterval(_interval);
        sequence.Append(transform.DOLocalMove(_startPosition, Duration));
        sequence.SetLoops(-1, LoopType.Restart).SetEase(MotionCurve);
    }
}
