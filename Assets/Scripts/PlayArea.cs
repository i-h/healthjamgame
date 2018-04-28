using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class PlayArea : MonoBehaviour {
    Collider _c;
    Rigidbody _returnable;
    Vector3 _returnPos;

    private void Awake()
    {
        _c = GetComponent<Collider>();
        _c.isTrigger = true;
    }

    private void Update()
    {
        if(_returnable != null)
        {
            Vector3 diff = (_returnPos + transform.position*0.1f) - _returnable.transform.position;
            _returnable.velocity += diff.normalized * 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb != null)
            {
                MissionPlayerControl.ControlsEnabled = false;
                _returnable = rb;
                _returnPos = transform.position;
                _returnPos.y = _returnable.transform.position.y;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _returnable != null)
        {
            StartCoroutine(SlowDown(_returnable));
            _returnable = null;
        }
    }

    IEnumerator SlowDown(Rigidbody rb)
    {
        MissionPlayerControl.ControlsEnabled = false;
        float factor = 1;
        while(rb.velocity.magnitude > 0.1f)
        {
            rb.velocity /= factor;
            factor += 0.01f;
            Debug.Log(rb.velocity.magnitude);
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = Vector3.zero;
        MissionPlayerControl.ControlsEnabled = true;
    }
}
