using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Toothbrush : MonoBehaviour {
    public float BrushForce = 0;
    Vector3 _prevPos;
    Vector3 _currPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _prevPos = _currPos;
        _currPos = transform.position;
        BrushForce = (_prevPos - _currPos).magnitude;
	}
    private void OnGUI()
    {
        GUILayout.Label(BrushForce + "");
    }
}
