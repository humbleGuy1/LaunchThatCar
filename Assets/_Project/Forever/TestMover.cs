using UnityEngine;
using Dreamteck.Forever;

public class TestMover : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _moveWidth;

    private Runner _runner;
    private float _input;
    private Vector2 _startOffset;

    private void Start()
    {
        _runner = GetComponent<Runner>();
        _startOffset = _runner.motion.offset;
    }

    private void Update()
    {
        _input += Input.GetAxis("Mouse X") * _sensitivity;
        _input = Mathf.Clamp(_input, -1f, 1f);
        Vector2 moveVector = Vector2.right * _input * _moveWidth * 0.5f;
        _runner.motion.offset = _startOffset + moveVector;
    }
}
