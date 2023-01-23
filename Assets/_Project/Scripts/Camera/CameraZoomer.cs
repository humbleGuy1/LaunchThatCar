using UnityEngine;
using Cinemachine;
using Runtime.BaseCar;
using System.Collections;

public class CameraZoomer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CarMover _carMover;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField, Range(0, 60f)] private float _minFOV;
    [SerializeField] private float _zoomOutTime;

    private float _currentFOV;
    private float _currentTime;
    private readonly float _maxFOV = 60f;

    private void Update()
    {
        if(_playerInput.IsButtonHold)
            ZoomIn();

        if (_playerInput.IsButtonUp)
            StartCoroutine(ZoomingOut());
    }

    public void ZoomIn()
    {
        _camera.m_Lens.FieldOfView = Mathf.Lerp(_maxFOV, _minFOV, _carMover.Speed/_carMover.MaxSpeed);
        _currentFOV = _camera.m_Lens.FieldOfView;
    }

    private IEnumerator ZoomingOut()
    {
        while(_currentTime <= _zoomOutTime)
        {
            _camera.m_Lens.FieldOfView = Mathf.Lerp(_currentFOV, _maxFOV, _currentTime / _zoomOutTime);
            _currentTime += Time.deltaTime;

            yield return null;
        }

        _currentTime = 0f;
        _camera.m_Lens.FieldOfView = _maxFOV;
    }
}
