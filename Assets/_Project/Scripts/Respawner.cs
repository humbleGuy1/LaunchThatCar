using Runtime.BaseCar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private PlayerInput _playterInput;
    [SerializeField] protected RespawnPoint _respawnPoint;

    public RespawnPoint CurrentRespawnPoint;

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
        CurrentRespawnPoint = respawnPoint;
    }

    public void Respawn()
    {
        _respawnable.Respawn(CurrentRespawnPoint.SpawnPoint.transform);
    }
}
