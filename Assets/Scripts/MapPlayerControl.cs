using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayerControl : MonoBehaviour {
    public static bool Disabled = false;
    public static ToothNode CurrentNode;
    public static string CurrentNodeName;
    private List<ToothNode> _path;
    public ToothNode StartNode;
    Vector3 _pointerPos;
    bool _movable = true;

    private void Start()
    {
        Debug.Log(CurrentNode);
        GameObject g = GameObject.Find(CurrentNodeName);
        if (g != null) CurrentNode = g.GetComponent<ToothNode>();
        if (StartNode != null && CurrentNode == null)
        {
            SetNode(StartNode);
        } else if (CurrentNode != null)
        {
            SetNode(CurrentNode);
        }
    }

    private void Update()
    {
        if (MainMenuControl.Displayed) return;
        _pointerPos = Input.mousePosition;
        if (Input.touchCount > 0)
            _pointerPos = Input.GetTouch(0).position;
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(_pointerPos), out hit, int.MaxValue, LayerMask.GetMask("Node")))
            {
                ToothNode n = hit.collider.GetComponent<ToothNode>();
                if (n != null && _movable)
                {
                    if (CurrentNode == n)
                    {
                        SceneManager.LoadScene("tooth_mission");
                    }
                    else
                    {
                        SetNode(n);
                    }
                }
            }
        }
    }

    public void SetNode(ToothNode newCurrent)
    {
        CurrentNodeName = newCurrent.name;
        if (CurrentNode == null)
        {
            StartCoroutine(MoveBetween(transform.position, newCurrent.GetPlayerPosition()));
        }
        else
        {
            StartCoroutine(MoveBetween(CurrentNode.GetPlayerPosition(), newCurrent.GetPlayerPosition()));
        }
        CurrentNode = newCurrent;
    }

    IEnumerator MoveBetween(Vector3 oldNode, Vector3 newNode)
    {
        _movable = false;
        float t = 0;
        while(t < 1)
        {
            transform.position = Vector3.Lerp(oldNode, newNode, t);
            t += 0.01f;
            yield return new WaitForEndOfFrame();
        }
        transform.position = newNode;
        _movable = true;
    }
}
