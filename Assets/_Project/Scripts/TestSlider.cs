using Runtime.BaseCar;
using UnityEngine;
using UnityEngine.UI;

public class TestSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private CarMover _carEngine;

    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = 100;
    }

    private void Update()
    {
        _slider.maxValue = _carEngine.MaxForce;
        _slider.value = _carEngine.Force;
    }
}
