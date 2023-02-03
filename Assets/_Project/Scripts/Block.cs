#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private RespawnPoint _respawnPointPrefab;
    [SerializeField] private Ramp _rampPrefab;
    [Header("Spawned Objects")]
    [SerializeField] private RespawnPoint _createdRespawnPoint;
    [SerializeField] private Ramp _createdRamp;

    public void AddRespawnPoint()
    {
        if (_createdRespawnPoint != null)
            return;

        var respawnPoint = Instantiate(_respawnPointPrefab, transform);
        respawnPoint.transform.localPosition = Vector3.up * 3;
        _createdRespawnPoint = respawnPoint;

        PrefabUtility.RecordPrefabInstancePropertyModifications(this);
    }

    public void RemoveRespawnPoint()
    {
        DestroyImmediate(_createdRespawnPoint.gameObject);
    }

    public void AddRamp()
    {
        if (_createdRamp != null)
            return;

        var ramp = Instantiate(_rampPrefab, transform);
        ramp.transform.localPosition = new Vector3(0, -4.5f, 51f);
        _createdRamp = ramp;

        PrefabUtility.RecordPrefabInstancePropertyModifications(this);
    }

    public void RemoveRamp()
    {
        DestroyImmediate(_createdRamp.gameObject);
    }
}
# endif