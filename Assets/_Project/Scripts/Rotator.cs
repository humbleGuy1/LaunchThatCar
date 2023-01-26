using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private GameObject _platform;

    private void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, _platform.transform.rotation.z,
            transform.rotation.w);
    }
}
