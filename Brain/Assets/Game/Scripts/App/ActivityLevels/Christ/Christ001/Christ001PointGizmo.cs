using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Christ001PointGizmo : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach(Transform child in transform)
        {
            Gizmos.DrawWireSphere(child.position, 5.0f);
        }
    }
}
