using UnityEngine;

public class MaxStopSpeedSlider : MonoBehaviour
{
    [SerializeField] private CarMover _carMover;
    [SerializeField] private SliderView _sliderView;

    private void Update()
    {
        _sliderView.SetSliderValue(_carMover.MaxStopSpeed, _carMover.MaxSpeed);
    }
}
