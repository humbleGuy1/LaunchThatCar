using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "RampType", menuName = "ScriptableObject/RampType", order = 1)]
public class RampsTypes : ScriptableObject
{
    [SerializeField] private List<RampProperty> _rampPropertys;

    public RampProperty GetPropertyByType(RampType rampType)
    {
        return _rampPropertys.First(property => property.Type == rampType);
    }
}
