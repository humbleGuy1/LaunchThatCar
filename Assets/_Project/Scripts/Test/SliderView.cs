using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _view;
    [SerializeField] private string _text;

    private void Update()
    {
        _view.text = $"{_text} {_slider.value.ToString("0.0")}";
    }

    public void SetSliderValue(float current, float max)
    {
        _slider.value = current;
        _slider.maxValue = max;
    }
}
