using UnityEngine;
using System.Collections;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private float _capacity;
    [SerializeField, Min(0)] private float _startValue;
    [SerializeField] private float _consuption;

    private float _currentValue;

    public float CurrentValue => _currentValue;
    public float Capacity => _capacity;

    private void Start()
    {
        _currentValue = _startValue;
        StartCoroutine(ReducingFuel());
    }

    public void Add(int value)
    {
        _currentValue += value;
        _currentValue = Mathf.Clamp(_currentValue, 0, _capacity);
        print(_currentValue);
    }

    private IEnumerator ReducingFuel()
    {
        while (true)
        {
            _currentValue = Mathf.MoveTowards(_currentValue, 0, _consuption * Time.deltaTime);

            yield return null;
        }
    }
}
