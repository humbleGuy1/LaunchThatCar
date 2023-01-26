using Runtime.BaseCar;
using UnityEngine;
using UnityEngine.UI;

public class TestSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private CarMover _carEngine;

    private void Update()
    {
        _slider.maxValue = _carEngine.MaxSpeed;
        _slider.value = _carEngine.Speed;
    }
}
