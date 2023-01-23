using UnityEngine;

public class RampSlider: MonoBehaviour
{
    [SerializeField] private Ramp _ramp;
    [SerializeField] private SliderView _angleSlider;
    [SerializeField] private SliderView _distanceSlider;

    private void Start()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        _angleSlider.SetSliderValue(_ramp.CurrentProperty.XAngle, 87);
        _distanceSlider.SetSliderValue(_ramp.CurrentProperty.Distance, 30);
    }
}
