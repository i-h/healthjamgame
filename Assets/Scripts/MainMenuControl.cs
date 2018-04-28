using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {
    public static bool Displayed = false;
    public Image Background;
    public Transform MainMenuRoot;
    public Transform SettingsRoot;

    public void FadeInMenu(bool fast)
    {
        Displayed = true;
        MainMenuRoot.gameObject.SetActive(true);
        SettingsRoot.gameObject.SetActive(false);
        if(fast) StartCoroutine(FadeBackground(0, 1, 1.0f));
        else StartCoroutine(FadeBackground(0, 1, 0.05f));

    }

    public void FadeOutMenu()
    {
        Displayed = false;
        MainMenuRoot.gameObject.SetActive(false);
        SettingsRoot.gameObject.SetActive(false);
        StartCoroutine(FadeBackground(1, 0, 0.05f));
    }

    public void ShowSettings()
    {
        MainMenuRoot.gameObject.SetActive(false);
        SettingsRoot.gameObject.SetActive(true);
    }

    public void HideSettings()
    {
        MainMenuRoot.gameObject.SetActive(true);
        SettingsRoot.gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

	void Start () {
        if (!Displayed) FadeInMenu(true);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) FadeInMenu(false);
	}

    IEnumerator FadeBackground(float start, float end, float step)
    {
        Background.gameObject.SetActive(true);
        Color c = Background.color;
        float t = 0;
        while(t < 1.0)
        {
            c.a = Mathf.Lerp(start, end, t);
            Background.color = c;
            t += step;
            yield return new WaitForEndOfFrame();
        }
        c.a = end;
        Background.color = c;
    }    
}
