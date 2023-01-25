using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private CarController _carController;
    [SerializeField] private SliderView _sliderView;

    private void Update()
    {
        _sliderView.SetSliderValue(_carController.RotationSensitivity, 4);
    }
}
