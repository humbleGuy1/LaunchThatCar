using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

[SelectionBase]
public class RotatingBridge : MonoBehaviour, ILoopedTrap
{
    [SerializeField] private float _interval;
    [SerializeField] private float _resetTime;
    [SerializeField] private List<Ground> _platforms;

    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public AnimationCurve MotionCurve { get; private set; }

    private void Start()
    {
        StartLoop();
    }

    public void StartLoop()
    {
        StartCoroutine(Rotating());
    }

    private IEnumerator Rotating()
    {
        while (true)
        {
            foreach (var platform in _platforms)
            {
                platform.transform.DOLocalRotate(new Vector3(0, 0, platform.transform.rotation.z - 180), 
                    Duration, RotateMode.FastBeyond360).SetEase(MotionCurve);

                yield return new WaitForSeconds(_interval);
            }

            yield return new WaitForSeconds(_resetTime);
        }
    }
}
