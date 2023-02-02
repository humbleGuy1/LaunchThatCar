using UnityEngine;
using DG.Tweening;
using System.Collections;

[SelectionBase]
public class MovingBlock : MonoBehaviour, ILoopedTrap
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _startDelay;
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
        StartCoroutine(SetDelay());
    }

    private IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(_startDelay);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rigidbody.DOMove(_targetPoint.position, Duration));
        sequence.AppendInterval(_interval);
        sequence.Append(_rigidbody.DOMove(_startPosition, Duration));
        sequence.AppendInterval(_interval);
        sequence.SetLoops(-1, LoopType.Restart).SetEase(MotionCurve);
    }
}
