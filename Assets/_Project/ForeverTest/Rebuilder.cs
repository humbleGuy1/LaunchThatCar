using UnityEngine;
using Dreamteck.Splines;

public class Rebuilder : MonoBehaviour
{
    [SerializeField] private SplineComputer _computer;

    private void Awake()
    {
        _computer.RebuildImmediate();
    }
}
