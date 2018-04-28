using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothNode : MonoBehaviour {
    public Vector3 GetPlayerPosition()
    {
        PlayerPosition pp = GetComponentInChildren<PlayerPosition>();
        if (pp != null)
        {
            return pp.transform.position;
        }
        else
        {
            return transform.position + Vector3.up * 0.5f;
        }
    }
}
