using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Ground ground))
        {
            IsGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            IsGrounded = false;
    }
}
