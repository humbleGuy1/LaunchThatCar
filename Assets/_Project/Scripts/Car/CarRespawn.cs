using System.Collections;
using UnityEngine;

public class CarRespawn
{
    public bool IsRespawning { get; private set; }

    private Rigidbody _rigidBody;
    private Transform _transform;
    private WheelsHandler _wheelStatus;

    public CarRespawn(Rigidbody rigidBody, Transform transform, WheelsHandler wheelStatus)
    {
        _rigidBody = rigidBody;
        _transform = transform;
        _wheelStatus = wheelStatus;
    }

    public IEnumerator Respawn(Transform point)
    {
        IsRespawning = true;
        _transform.position = point.position;
        _transform.rotation = point.rotation;
        _rigidBody.isKinematic = true;
        _wheelStatus.Stop();
        yield return null;
        _rigidBody.isKinematic = false;
        yield return new WaitForSeconds(0.4f);
        //yield return new WaitForSeconds(0.7f);
        _wheelStatus.Resume();
        IsRespawning = false;
    }
}
