using Runtime.BaseCar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private PlayerInput _playterInput;
    [SerializeField] protected RespawnPoint _respawnPoint;

    private IRespawnable _respawnable;

    private void Awake()
    {
        _respawnable = GetComponent<IRespawnable>();
    }

    private void Update()
    {
        if (_playterInput.SpacePressed)
            Respawn();
    }

    public void Respawn()
    {
        _respawnable.Respawn(_respawnPoint.transform);
    }
}
