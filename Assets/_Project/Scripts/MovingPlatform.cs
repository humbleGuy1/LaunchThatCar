using UnityEngine;
using DG.Tweening;
using Runtime.BaseCar;

[SelectionBase]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Transform _endValue;
    [SerializeField] private AnimationCurve _motionCurve;

    private Vector3 _startValue;

    private void Start()
    {
        //_startValue = transform.position;

        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(transform.DOMove(_endValue.position, _duration));    
        //sequence.Append(transform.DOMove(_startValue, _duration)); 
        //sequence.SetLoops(-1, LoopType.Restart).SetEase(_motionCurve);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Car car))
        {
            car.AttachToPlatform(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            car.DetachFromPlatform();
        }
    }
}
