using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTest : MonoBehaviour
{
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private float range;
    [SerializeField]
    private LayerMask attackMask;

    private void FixedUpdate()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(origin.position, range, attackMask);

        foreach (Collider2D col in cols)
        {
            Debug.Log(col);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin.position, range);
    }
}
