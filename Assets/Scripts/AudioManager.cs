using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public AudioClip BGMusic;
    public static AudioSource Source;
    public static AudioSource MusicSource;
	// Use this for initialization
	void Awake () {
        Source = GetComponent<AudioSource>();
        MusicSource = gameObject.AddComponent<AudioSource>();
	}

    private void Start()
    {
        MusicSource.clip = BGMusic;
        MusicSource.loop = true;
        MusicSource.volume = 0.7f;
        MusicSource.Play();
    }
    public static void PlaySound(AudioClip sound)
    {
        PlaySound(sound, 1.0f);
    }
    public static void PlaySound(AudioClip sound, float volume)
    {
        if (Source == null) return;
        Source.PlayOneShot(sound, volume);
        Debug.Log("Playing " + sound.name);
    }
}
