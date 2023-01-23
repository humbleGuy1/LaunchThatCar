using System.Collections;
using UnityEngine;

public class CarRespawn
{
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
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularDrag = 0;
        _rigidBody.angularVelocity = Vector3.zero;
        _transform.position = point.position;
        _transform.rotation = point.rotation;
        _rigidBody.isKinematic = true;
        _wheelStatus.Stop();
        yield return new WaitForSeconds(0.3f);
        _rigidBody.isKinematic = false;
        _wheelStatus.Resume();  
    }
}
