using System.Collections;
using UnityEngine;

public class CarRespawn
{
    public bool IsRespawning { get; private set; }

    private Rigidbody _rigidBody;
    private WheelsHandler _wheels;
    private Transform _transform;

    public CarRespawn(Rigidbody rigidBody, Transform transform,  WheelsHandler _wheelsHandler)
    {
        _transform = transform;
        _rigidBody = rigidBody;
        _wheels = _wheelsHandler;
    }

    public IEnumerator Respawn(Transform point)
    {
        yield return Respawn(point.position, point.rotation);
    }

    public IEnumerator Respawn(Vector3 position, Quaternion rotation)
    {
        IsRespawning = true;
        _transform.position = position;
        _transform.rotation = rotation;
        _rigidBody.isKinematic = true;
        //_wheels.Stop();
        yield return null;
        _rigidBody.isKinematic = false;
        yield return new WaitForSeconds(0.4f);
        //_wheels.Resume();
        IsRespawning = false;
    }
}
