using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPasteScript : MonoBehaviour {

    public bool endTip = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "Tube") 
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;

            if(endTip) 
            {
                Camera.main.GetComponent<InputScript>().Detach();
            }
        }
    }
}
