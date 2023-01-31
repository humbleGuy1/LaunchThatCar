using System.Collections.Generic;
using UnityEngine;

public class PropellerSwithcer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _blades;
    [SerializeField, Range(1, 4)] private int _bladesNumber;

    [ContextMenu(nameof(SwitchBlades))]
    public void SwitchBlades()
    {
        int counter = 0;

        foreach(var blade in _blades)
        {
            if (counter >= _bladesNumber)
                blade.SetActive(false);
            else
                blade.SetActive(true);

            counter++;
        }
    }
}
