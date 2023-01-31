using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Mass")]
    [SerializeField] private float _startMass;
    [SerializeField] private float _targetMass;
    [Header("Velocity")]
    [SerializeField] private float _maxVelocity;

    private void Start()
    {
        _rigidbody.mass = _startMass;
    }

    private void Update()
    {
        _rigidbody.mass = Mathf.Lerp(_startMass, _targetMass, _rigidbody.velocity.magnitude/_maxVelocity);
    }
}
