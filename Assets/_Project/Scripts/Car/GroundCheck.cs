using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool Grounded { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        Grounded = other.TryGetComponent(out Ground ground);
    }
}
