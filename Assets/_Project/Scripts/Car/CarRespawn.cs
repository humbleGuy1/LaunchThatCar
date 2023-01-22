using System.Collections;
using UnityEngine;

public class CarRespawn
{
    private Rigidbody _rigidBody;
    private Transform _transform;

    public CarRespawn(Rigidbody rigidBody, Transform transform)
    {
        _rigidBody = rigidBody;
        _transform = transform;
    }

    public IEnumerator Respawn(Transform point)
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularDrag = 0;
        _rigidBody.angularVelocity = Vector3.zero;
        _transform.position = point.position;
        _transform.rotation = point.rotation;
        _rigidBody.isKinematic = true;
        yield return new WaitForSeconds(0.2f);
        _rigidBody.isKinematic = false;
    }
}
