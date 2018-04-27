using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour {

    public Transform ink;
    public Transform brush;

    public bool brushOn = true;

    public Text buttonText;
    public string brushText = "Brush";
    public string pasteText = "Paste";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection( Vector3.forward );

        if(Physics.Raycast(ray, out hit,1000f))
        {
            if(Input.GetMouseButton(0)) 
            {
                if(brushOn && hit.transform.name == "Teeth") 
                {
                    Instantiate( ink, hit.point, transform.rotation );
                }

                else if(!brushOn && hit.transform.tag == "Ink") 
                {
                    hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }

        if(brushOn) {
            buttonText.text = brushText;
        }
        if(!brushOn) {
            buttonText.text = pasteText;
        }
		
	}

    public void Brush() 
    {
        brushOn = !brushOn;
    }
}
