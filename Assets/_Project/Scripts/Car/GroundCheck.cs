using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool Grounded { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Ground ground))
            Grounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            Grounded = false;
    }
}
