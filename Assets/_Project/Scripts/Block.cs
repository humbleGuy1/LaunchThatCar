using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private RespawnPoint _respawnPointPrefab;
    [SerializeField] private Ramp _rampPrefab;

    private RespawnPoint _respawnPoint;
    private Ramp _ramp;

    public void AddRespawnPoint()
    {
        if (_respawnPoint != null)
            return;

        var respawnPoint = Instantiate(_respawnPointPrefab, transform);
        respawnPoint.transform.localPosition = Vector3.up * 3;
        _respawnPoint = respawnPoint;
    }

    public void RemoveRespawnPoint()
    {
        if (_respawnPoint == null)
            return;

        DestroyImmediate(_respawnPoint.gameObject);
    }

    public void AddRamp()
    {
        if (_ramp != null)
            return;

        var ramp = Instantiate(_rampPrefab, transform);
        ramp.transform.localPosition = new Vector3(0, -4.5f, 51f);
        _ramp = ramp;
    }

    public void RemoveRamp()
    {
        if (_ramp == null)
            return;

        DestroyImmediate(_ramp.gameObject);
    }
}
