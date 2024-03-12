using UnityEngine;

[ExecuteAlways]
public class Barrel : MonoBehaviour
{
    [SerializeField, Range(0, 8f)] private float _radius;
    [SerializeField] private Color _color;
    [SerializeField] private MeshRenderer _meshRenderer;

    private MaterialPropertyBlock _materialProperty;

    private static readonly int shPropertyColor = Shader.PropertyToID("_Color");

    public MaterialPropertyBlock MaterialProperty
    {
        get 
        {
            if(_materialProperty == null)
                _materialProperty = new MaterialPropertyBlock();

            return _materialProperty;
        }
    }

    private void OnValidate()
    {
        ApplyColor();    
    }

    private void OnEnable()
    {
        ApplyColor();
        BarrelManager.BarrelList.Add(this);
    }

    private void OnDisable() => BarrelManager.BarrelList.Remove(this);

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void ApplyColor()
    {
        MaterialProperty.SetColor(shPropertyColor, _color);
        _meshRenderer.SetPropertyBlock(MaterialProperty);
    }
}
