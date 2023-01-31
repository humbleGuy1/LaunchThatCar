using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private AimPoint _aimPoint;
    [SerializeField] private Projectile _cannonball;
    [SerializeField] private float _cannonballSpeed;
    [SerializeField] private float _delay;
    [SerializeField] private float _offset;

    private void Awake()
    {
        StartCoroutine(Shooting());
    }

    private void Shoot()
    {
        var cannonBall = Instantiate(_cannonball, _aimPoint.transform.position, Quaternion.identity);
        cannonBall.Shoot(_aimPoint.transform.forward, _cannonballSpeed);
    }

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(_offset);

        while (true)
        {
            Shoot();

            yield return new WaitForSeconds(_delay);
        }
    }
}
