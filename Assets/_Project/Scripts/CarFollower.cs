using Runtime.BaseCar;
using UnityEngine;

[ExecuteAlways]
public class CarFollower : MonoBehaviour
{
    [SerializeField] private Car _car;
    private void Update()
    {
        transform.position = _car.transform.position;
    }
}
