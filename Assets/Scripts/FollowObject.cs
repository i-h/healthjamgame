using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowObject : MonoBehaviour {
    public Transform Target;
    public Vector3 Offset;
    private Transform t;
	// Use this for initialization
	void Start () {
		if(Target == null)
        {
            Debug.LogWarning("FollowObject script target null");
            this.enabled = false;
        }
        t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        t.position = Target.position + Offset;
	}
}
