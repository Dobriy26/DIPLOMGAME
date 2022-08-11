using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private float _radius;
    [SerializeField]
    private LayerMask _layerMask;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, _radius,_layerMask);
    }
}