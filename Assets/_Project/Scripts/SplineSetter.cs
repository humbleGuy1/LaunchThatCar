using Dreamteck.Splines;
using UnityEngine;

public class SplineSetter : Interactable
{
    [SerializeField] private SplineComputer _spine;
    [SerializeField] private bool _attach;

    public override void OnInteract(Car car)
    {
        if(_attach)
            car.CarMover.Controller.SetSplineToFollow(_spine);
        else
            car.CarMover.Controller.ResetSpline();
    }
}
