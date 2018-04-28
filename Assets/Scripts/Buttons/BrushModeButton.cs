using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushModeButton : BaseButton {
    bool pushed = false;
    public override void OnButtonDown()
    {
        if (pushed) return;
        pushed = true;
        Debug.Log("Clicked!");
        MissionPlayerControl.SwitchBrushMode();
        StartCoroutine(Flip());
    }

    IEnumerator Flip()
    {
        RotationJitter j = GetComponent<RotationJitter>();
        if (j != null) j.enabled = false;
        float t = 0;
        Vector3 start = transform.rotation.eulerAngles;
        Vector3 end = start + Vector3.right * 180;
        while(t < 1)
        {
            transform.eulerAngles = Vector3.Slerp(start, end, t);
            t += 0.05f;
            yield return new WaitForEndOfFrame();
        }
        transform.localEulerAngles = end;
        pushed = false;
        if (j != null) j.enabled = true;
    }
}
