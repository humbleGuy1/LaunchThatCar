using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shifting : MonoBehaviour
{
    [SerializeField] private float _timeOffset;
    [Header("Y")]
    [SerializeField] private float _yDistance;
    [SerializeField] private float _ySpeed;
    [Header("X")]
    [SerializeField] private float _xDistance;
    [SerializeField] private float _xSpeed;
    [Header("Z")]
    [SerializeField] private float _zDistance;
    [SerializeField] private float _zSpeed;
    [SerializeField] private float _xRotationSpeed;
    [SerializeField] private float _yRotationSpeed;
    [SerializeField] private float _zRotationSpeed;

    private float yPos = 0;
    private float xPos = 0;
    private float zPos = 0;

    private void Awake()
    {
        StartCoroutine(Shift());        
    }

    private IEnumerator Shift()
    {
        if(_timeOffset>0)
            yield return new WaitForSeconds(_timeOffset);

        float elapsedTime = 0;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            yPos = CalculatePosition(_ySpeed, _yDistance, elapsedTime);
            xPos = CalculatePosition(_xSpeed, _xDistance, elapsedTime);
            zPos = CalculatePosition(_zSpeed, _zDistance, elapsedTime);


            transform.localPosition = new Vector3(xPos, yPos, zPos);
            transform.localRotation *= Quaternion.Euler(_xRotationSpeed*Time.deltaTime, _yRotationSpeed*Time.deltaTime, _zRotationSpeed*Time.deltaTime);
            yield return null;
        }
    }

    private float CalculatePosition(float speed, float distacne, float elapsedTime)
    {
        return distacne * Mathf.Sin(speed * elapsedTime);
    }
}
