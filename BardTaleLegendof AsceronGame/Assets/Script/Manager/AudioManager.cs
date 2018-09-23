using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Use this for initialization
    void Start () {
        Initialize();
        DontDestroyOnLoad(this);
	}

    private void Initialize()
    {
        SetVolume("masterVol", PlayerPrefs.GetFloat("masterVol"));
        SetVolume("musicVol", PlayerPrefs.GetFloat("musicVol"));
        SetVolume("voiceVol", PlayerPrefs.GetFloat("voiceVol"));
        SetVolume("sfxVol", PlayerPrefs.GetFloat("sfxVol"));
    }

    private void SetVolume(string channel, float volume)
    {
        audioMixer.SetFloat(channel, volume);
    }
}
