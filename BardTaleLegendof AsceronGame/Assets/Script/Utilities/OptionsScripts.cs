using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsScripts : MonoBehaviour {
    public AudioMixer audioMixer;

    public void SetMasterVolume (float volume)
    {
        SetVolume("masterVol", volume);
    }

    public void SetMusicVolume(float volume)
    {
        SetVolume("musicVol", volume);
    }

    public void SetVoiceVolume(float volume)
    {
        SetVolume("voiceVol", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetVolume("sfxVol", volume);
    }

    private void SetVolume(string channel, float volume)
    {
        audioMixer.SetFloat(channel, volume);
        PlayerPrefs.SetFloat(channel, volume);
    }
}
