using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour {

    public Transform ink;
    public Transform brush;
    public Transform tube;
    public bool tubeEmpty = false;
    public Transform toothPaste;
    public Transform newToothPaste;


    public bool brushOn = true;

    public Text buttonText;
    public string brushText = "Brush";
    public string pasteText = "Paste";

    public AudioClip splurt;

    private AudioSource myAudio;

	// Use this for initialization
	void Start () {
        myAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection( Vector3.forward );

        if(Physics.Raycast(ray, out hit,1000f))
        {

                if(brushOn) 
                {
                    tube.position = new Vector3(hit.point.x+1f,hit.point.y,-5.767f);
                    brush.position = new Vector3(5f,0f,-5f);

                    if ( Input.GetMouseButton( 0 ) ) 
                    {
                        toothPaste.Translate( Vector3.forward * -1f * Time.deltaTime );
                        if(!myAudio.isPlaying) 
                        {
                        }
                        //Instantiate( ink, hit.point, transform.rotation );
                    }
                if ( Input.GetMouseButtonDown( 0 ) ) 
                    {
                        myAudio.PlayOneShot( splurt );

                    }

                }

                else if(!brushOn) 
                {
                    brush.position = new Vector3(hit.point.x+1f,hit.point.y,hit.point.z-0.1f);
                    if(hit.transform.tag == "Ink") 
                    {
                        hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    }

                }
        }

        if(brushOn) 
        {
            buttonText.text = brushText;
            brush.position = new Vector3(5f,0f,-5f);

        }

        if(!brushOn) 
        {
            buttonText.text = pasteText;
        }
		
        if(tubeEmpty) 
        {
            Transform newPaste = Instantiate( newToothPaste, tube.position, tube.rotation);
            toothPaste = newPaste;
            newPaste.parent = tube;
            tubeEmpty = false;
        }

	}

    public void Brush() 
    {
        brushOn = !brushOn;
    }

    public void Detach() {
        toothPaste.parent = null;
        tubeEmpty = true;

    }
}
