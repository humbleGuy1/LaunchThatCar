using Cinemachine;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _defaultPoint;

    public void ResetPosition()
    {
        _camera.ForceCameraPosition(_defaultPoint.position, Quaternion.identity);
    }
}
