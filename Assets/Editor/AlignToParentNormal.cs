using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AlignToParentNormal : MonoBehaviour {
    [MenuItem("Align/Selected")]
    static void AlignSelectedToParent()
    {
        Transform c = Selection.activeTransform;
        if (c == null) return;
        Vector3 p = c.parent.position;
        p.y = c.position.y;
        c.LookAt(p);
        RaycastHit hit;
        if(Physics.Raycast(c.position, p - c.position, out hit, float.MaxValue))
        {
            c.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
        }
    }
}
