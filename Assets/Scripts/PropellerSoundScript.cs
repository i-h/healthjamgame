using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSoundScript : MonoBehaviour {
    public AudioClip Sound;
    public AudioSource PropellerSource;

    private void Start()
    {
        PropellerSource = AudioManager.Source.gameObject.AddComponent<AudioSource>();
        PropellerSource.clip = Sound;
        PropellerSource.volume = 0.3f;
    }
    void PlayPropellerClip()
    {
        if (PropellerSource == null || MainMenuControl.Displayed) return;
        PropellerSource.Play();
    }
}
