using Runtime.BaseCar;
using UnityEngine;
using UnityEngine.UI;

public class TestSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = 100;
    }

    private void Update()
    {
        if(_playerInput.IsButtonHold)
            _slider.value = _playerInput.DeltaY;

        if (_playerInput.IsButtonUp)
            _slider.value = _slider.minValue;
    }
}
