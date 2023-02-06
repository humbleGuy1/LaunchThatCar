using Dreamteck.Splines;
using Runtime.BaseCar;
using UnityEngine;

public class SplineSetter : Interactable
{
    [SerializeField] private SplineComputer _spine;
    [SerializeField] private bool _attach;

    public override void OnInteract(Car car)
    {
        if(_attach)
            car.PositionProperty.SetSplineToFollow(_spine);
        else
            car.PositionProperty.ResetSpline();
    }
}
