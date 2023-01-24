using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public void CollectCheckPoint()
    {
        Material material = new Material(_meshRenderer.material);
        material.color = Color.green;
        _meshRenderer.material = material;
    }
}
