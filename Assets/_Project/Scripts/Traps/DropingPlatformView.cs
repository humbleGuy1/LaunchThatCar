using DG.Tweening;
using UnityEngine;

public class DropingPlatformView: MonoBehaviour
{
    public void Shake(float duration)
    {
        transform.DOShakePosition(duration,0.1f,60,180);
    }
}
