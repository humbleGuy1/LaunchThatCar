using DG.Tweening;
using Runtime.BaseCar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropingPlatform : Interactable
{
    [SerializeField] private float _dropDelay;
    [SerializeField] private float _resetTime;
    [SerializeField] private DropingPlatformView _view;
    [SerializeField] private Rigidbody _rigidbody;

    private bool _triggered;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    public override void OnInteract(Car car)
    {
        StartCoroutine(Droping());
    }

    private IEnumerator Droping()
    {
        if (_triggered)
            yield break;

        _triggered = true;
        _view.Shake(_dropDelay);

        yield return new WaitForSeconds(_dropDelay);

        _rigidbody.isKinematic = false;

        yield return new WaitForSeconds(_resetTime);

        _rigidbody.isKinematic = true;
        _triggered = false;
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}
