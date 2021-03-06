﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class MissionPlayerControl : MonoBehaviour {
    public static bool ControlsEnabled = true;
    public static bool BrushModeEnabled = false;

    public Transform ToothBrush;

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
    
    public void Win()
    {
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            AudioManager.MusicSource.pitch = 1 - t;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2);
        
        SceneManager.LoadScene("world_map");

    }
    
    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _levelMask = LayerMask.GetMask("Level", "UI");
        SwitchBrushMode(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenuControl.Displayed) return;
        ToothBrush.gameObject.SetActive(BrushModeEnabled);
        
        if (UpdatePointer())
        {
            if (ControlsEnabled) _target = WorldPointer;
            if (BrushModeEnabled) BrushMode();            
        }
        if ((_target - transform.position).magnitude > 0.1f)
        {
            _targetDir = _target - transform.position + (Vector3)Random.insideUnitCircle * TargetJitter;
            if (_rb.velocity.magnitude < _targetDir.magnitude)
                _rb.AddForce(_targetDir.normalized * _rb.drag * Force);
        }
    }

    void BrushMode()
    {
        if (ToothBrush == null) return;
        if (!ToothBrush.gameObject.activeInHierarchy) ToothBrush.gameObject.SetActive(true);
        ToothBrush.position = WorldPointer;
    }
    public static void SwitchBrushMode()
    {
        SwitchBrushMode(!BrushModeEnabled);
    }
    public static void SwitchBrushMode(bool on)
    {
            ControlsEnabled = !on;
            BrushModeEnabled = on;
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
                if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    BaseButton btn = hit.collider.GetComponentInParent<BaseButton>();
                    if (btn != null) btn.OnButtonDown();
                }

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
