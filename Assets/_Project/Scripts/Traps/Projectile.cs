using System.Collections;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public void Shoot(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);

        StartCoroutine(DieCount());
    }

    private IEnumerator DieCount()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}
