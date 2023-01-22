using UnityEngine;
using System;

[Serializable]
public class CenterOfMassPosition
{
    [SerializeField] private CenterOfMass _centerOfMass;
    [SerializeField] private MassPoint _forwardMassPoint;
    [SerializeField] private MassPoint _backMassPoint;
    [SerializeField] private AnimationCurve _curve;

    private PositionProperty _positionProperty;

    public void Init(PositionProperty positionProperty)
    {
        _positionProperty = positionProperty;
    }

    public Vector3 GetCenterOfMassPosition(bool isGrouned)
    {
        if (isGrouned)      
            _centerOfMass.transform.localPosition = _positionProperty.Down.localPosition;
        else
            _centerOfMass.transform.localPosition = _positionProperty.Up.localPosition;

        return _centerOfMass.transform.localPosition;
    }
}
