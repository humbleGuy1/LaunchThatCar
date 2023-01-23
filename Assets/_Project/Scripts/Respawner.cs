using Runtime.BaseCar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private PlayerInput _playterInput;
    [SerializeField] protected RespawnPoint _respawnPoint;

    private RespawnPoint _currentRespawnPoint;

    private IRespawnable _respawnable;

    private void Awake()
    {
        _respawnable = GetComponent<IRespawnable>();
        SetRespawnPoint(_respawnPoint);
    }

    private void Start()
    {
        Respawn();
    }

    private void Update()
    {
        if (_playterInput.SpacePressed)
            Respawn();
    }

    public void SetRespawnPoint(RespawnPoint respawnPoint)
    {
        _currentRespawnPoint = respawnPoint;
    }

    public void Respawn()
    {
        _respawnable.Respawn(_currentRespawnPoint.transform);
    }
}
