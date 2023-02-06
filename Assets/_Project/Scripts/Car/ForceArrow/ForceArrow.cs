using Runtime.BaseCar;
using UnityEngine;

public class ForceArrow : MonoBehaviour
{
    [SerializeField] private Transform _arrow;
    [SerializeField] private CarMover _carMover;
    [SerializeField] private float _maxSize;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Vector3 _initialScale;
    private Vector3 _targetScale;

    private void Awake()
    {
        _initialScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
        _targetScale = new Vector3(transform.localScale.x, transform.localScale.y, _maxSize) ;
    }

    private void Update()
    {
        float lerp = _carMover.ChargedSpeed / _carMover.MaxSpeed;
        _arrow.transform.localScale = Vector3.Lerp(_initialScale, _targetScale, lerp);
        _arrow.transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.forward * _maxSize/2, lerp);
        _meshRenderer.enabled = _arrow.transform.localScale.z > 0;

    }
}
