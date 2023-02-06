using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimator : MonoBehaviour
{
    [SerializeField] private float _wiggleSpeed;

    private float _lerp;
    private float _defaultLerp = 0.5f;
    private float _maxAngle = 10;
    private float _time;
    private Quaternion _leftWiggle;
    private Quaternion _rightWiggle;

    private void Awake()
    {
        _lerp = _defaultLerp;
    }

    public void AnimateWiggling(float chargedPercent)
    {
        UpdateLerp();
        float angle = Mathf.Lerp(0, _maxAngle, chargedPercent);
        SetWiggleAngle(angle);
        Rotate(_lerp);
    }

    public void ReleaseWiggling()
    {
        StartCoroutine(Defaulting());
    }

    private IEnumerator Defaulting() 
    {
        float elapsedTime = 0;
        float defaultingDuration = 0.3f;
        while(elapsedTime< defaultingDuration)
        {
            elapsedTime += Time.deltaTime;
            _lerp = Mathf.Lerp(_lerp, _defaultLerp, elapsedTime / defaultingDuration);
            Rotate(_lerp);

            yield return null;
        }

        _time = 0;
    }

    private void Rotate(float lerp)
    {
        transform.localRotation = Quaternion.Slerp(_rightWiggle, _leftWiggle, lerp);
    }

    private void UpdateLerp()
    {
        _time += Time.deltaTime*_wiggleSpeed;
        _lerp = (Mathf.Sin(_time)+1)/2;
    }

    private void SetWiggleAngle(float angle)
    {
        _leftWiggle = Quaternion.Euler(0, angle, 0);
        _rightWiggle = Quaternion.Euler(0, -angle, 0);
    }

}
