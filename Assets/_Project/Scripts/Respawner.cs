using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private PlayerInput _playterInput;
    [SerializeField] private RespawnPoint _respawnPoint;
    [SerializeField] protected CameraReset _reset;

    public RespawnPoint CurrentRespawnPoint { get; private set; }

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
        _reset.ResetPosition();
    }
}
