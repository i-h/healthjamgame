﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MissionPlayerControl : MonoBehaviour {
    public static bool ControlsEnabled = true;
    public static bool BrushModeEnabled = false;

    Vector3 _lastPointer;
    Vector3 _curPointer;
    Vector3 _target;
    Vector3 _targetDir;
    public Vector3 WorldPointer;
    public Vector3 WorldPointerDir;
    public float Force = 5.0f;
    public float TargetJitter = 0.5f;
    int _levelMask;
    Rigidbody _rb;
    
    // Use this for initialization
    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _levelMask = LayerMask.GetMask("Level", "UI");
        Debug.Log(System.Convert.ToString(_levelMask, 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdatePointer())
        {
            _target = WorldPointer;
            if (BrushModeEnabled)
            {

            }
        }
        if (ControlsEnabled && (_target - transform.position).magnitude > 0.1f)
        {
            _targetDir = _target - transform.position + (Vector3)Random.insideUnitCircle * TargetJitter;
            if (_rb.velocity.magnitude < _targetDir.magnitude)
                _rb.AddForce(_targetDir.normalized * _rb.drag * Force);
        }
    }

    public static void SwitchBrushMode()
    {
        if (BrushModeEnabled)
        {
            ControlsEnabled = true;
            BrushModeEnabled = false;
        } else
        {
            ControlsEnabled = false;
            BrushModeEnabled = true;
        }
    }

    bool UpdatePointer()
    {
        Vector3 pointerPos;

        pointerPos = Input.mousePosition;
        if (Input.touchCount > 0)
            pointerPos = Input.GetTouch(0).position;

        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(pointerPos), out hit, int.MaxValue, _levelMask))
            {
                BaseButton btn = hit.collider.GetComponentInParent<BaseButton>();
                if (btn != null) btn.OnButtonDown();

                if (hit.collider.tag == "Level")
                {
                    WorldPointer = hit.point;
                    WorldPointer.z = transform.position.z;
                }
            } else
            {
                //WorldPointer = transform.position + (Camera.main.ScreenToViewportPoint(pointerPos) * 2) - Vector3.up - Vector3.right;
            }
            WorldPointerDir = (WorldPointer - transform.position).normalized;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(WorldPointer, 1.0f);
    }
}
