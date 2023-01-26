using UnityEngine;
using Cinemachine;
using Runtime.BaseCar;

public class BlendingModeSwticher : MonoBehaviour
{
    [SerializeField] private Car _car;

    private CinemachineVirtualCamera _camera;
    private CinemachineTransposer _transposer;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        if (_car.CarMover.Wheels.IsGrounded)
        {
            _transposer.m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;
        }
        else
        {
            _transposer.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        }
        _transposer.m_FollowOffset.x = 0;
    }
}
