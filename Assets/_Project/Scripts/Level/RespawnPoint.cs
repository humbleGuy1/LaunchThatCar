using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RespawnPoint : MonoBehaviour
{
    [field: SerializeField] public SpawnPoint SpawnPoint { get; private set; }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Respawner respawner))
        {
            respawner.SetRespawnPoint(this);
            SpawnPoint.CollectCheckPoint();
        }
    }

}
