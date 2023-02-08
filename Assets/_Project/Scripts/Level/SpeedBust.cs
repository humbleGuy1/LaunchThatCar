using System.Collections;
using UnityEngine;

public class SpeedBust : Interactable
{
    [SerializeField] private float _speed = 200;
    [SerializeField] private float _targetTime;

    public override void OnInteract(Car car)
    {
        StartCoroutine(Moving(car));
    }

    private IEnumerator Moving(Car car)
    {
        float elapsedTime = 0;

        while(elapsedTime < _targetTime)
        {
            car.CarMover.MoveForward(_speed);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
