using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationJitter : MonoBehaviour {
    public float Speed = 1.0f;
    public Vector3 RotationRange = new Vector3(5, 5, 5);
    public Vector3 Jitter;
    Vector3 _offset;
    float _lastPhase;
    float _phase;
    float _lastDir;
    float _dir;

    private void Start()
    {
        Randomize();
    }

    private void Awake()
    {
        _offset = transform.rotation.eulerAngles;
    }

    void Update () {
        _lastPhase = _phase;
        _phase = Mathf.Sin(Time.time * Speed);
        _lastDir = _dir;
        _dir =  _phase - _lastPhase;

        if(Mathf.Sign(_dir) != Mathf.Sign(_lastDir))
        {
            //Randomize();
        }
        
        transform.localRotation = Quaternion.Euler(_offset + Jitter * _phase);
	}

    void Randomize()
    {
        Jitter = Vector3.Scale(RotationRange, (Random.insideUnitSphere+Vector3.one)/2);
    }
}
