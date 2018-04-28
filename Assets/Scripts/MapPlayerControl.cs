using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayerControl : MonoBehaviour {
    public static bool Disabled = false;
    private ToothNode _currentNode;
    private List<ToothNode> _path;
    public ToothNode StartNode;
    Vector3 _pointerPos;
    bool _movable = true;

    private void Start()
    {
        if(StartNode != null)
        {
            SetNode(StartNode);
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
                    if (_currentNode == n)
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
        if (_currentNode == null)
        {
            StartCoroutine(MoveBetween(transform.position, newCurrent.GetPlayerPosition()));
        }
        else
        {
            StartCoroutine(MoveBetween(_currentNode.GetPlayerPosition(), newCurrent.GetPlayerPosition()));
        }
        _currentNode = newCurrent;
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
