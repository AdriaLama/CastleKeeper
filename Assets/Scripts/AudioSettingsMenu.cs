using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        if (PersistentAudioManager.Instance != null)
            PersistentAudioManager.Instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (PersistentAudioManager.Instance != null)
            PersistentAudioManager.Instance.SetSFXVolume(volume);
    }
}

