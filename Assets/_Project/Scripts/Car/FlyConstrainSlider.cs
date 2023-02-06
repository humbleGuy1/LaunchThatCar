using Runtime.BaseCar;
using UnityEngine;

public class FlyConstrainSlider : MonoBehaviour
{
    [SerializeField] private SliderView _sliderView;
    [SerializeField] private CarMover _carMover;

    private void Update()
    {
        _sliderView.SetSliderValue(_carMover.MaxFlySpeed, _carMover.MaxSpeed);
    }
}
