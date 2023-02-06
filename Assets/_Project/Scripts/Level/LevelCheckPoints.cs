using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Runtime.BaseCar;

public class LevelCheckPoints : MonoBehaviour
{
    [SerializeField] private List<RespawnPoint> _respawnPoints;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private CarMover _carMover;

    private void Awake()
    {
        SetUp();
        _dropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < _respawnPoints.Count; i++)
        {
            options.Add(new TMP_Dropdown.OptionData($"CheckPoint {i}"));
        }

        _dropdown.AddOptions(options);
        _dropdown.onValueChanged.AddListener(OnDropDownChanged);
    }

    [ContextMenu(nameof(SetUp))]
    private void SetUp()
    {
        _respawnPoints = GetComponentsInChildren<RespawnPoint>().ToList();
    }

    public void OnDropDownChanged(int index)
    {
        _carMover.Respawn(_respawnPoints[index].SpawnPoint.transform);
    }
}
