using Runtime.BaseCar;
using UnityEngine;

public class StopSpeedSlider : MonoBehaviour
{
    [SerializeField] private CarMover _carMover;
    [SerializeField] private SliderView _sliderView;

    private void Update()
    {
        _sliderView.SetSliderValue(_carMover.StopSpeed, 400);
    }
}
