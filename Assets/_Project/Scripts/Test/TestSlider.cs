using UnityEngine;
using UnityEngine.UI;

public class TestSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private FuelTank _fuelTank;

    private void Update()
    {
        _slider.maxValue = _fuelTank.Capacity;
        _slider.value = _fuelTank.CurrentValue;
    }

    public void SetFuelTank(FuelTank fuelTank)
    {
        _fuelTank = fuelTank;
    }
}
