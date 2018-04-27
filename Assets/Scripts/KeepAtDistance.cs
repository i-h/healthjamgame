using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAtDistance : MonoBehaviour {
    public Transform Target;
    public float Distance = 3.0f;
    Ray _distanceRay = new Ray();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = Target.position - transform.position;

        _distanceRay.origin = transform.position;
        _distanceRay.direction = diff.normalized;
        
        RaycastHit hit;
        if(Physics.Raycast(_distanceRay, out hit, diff.magnitude))
        {
            transform.position = hit.point + hit.normal * Distance;
        }


    }
}
