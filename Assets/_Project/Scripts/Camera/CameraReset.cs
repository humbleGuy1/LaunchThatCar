using Cinemachine;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _position;

    public void ResetPosition()
    {
        _camera.ForceCameraPosition(_position.position, Quaternion.identity);
    }
}
