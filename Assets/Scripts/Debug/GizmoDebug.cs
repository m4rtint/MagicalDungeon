using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDebug : MonoBehaviour {

    void OnDrawGizmosSelected()
    {
        // Draw SPawn Points
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
