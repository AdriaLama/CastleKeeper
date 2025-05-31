using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PersistentAudioManager : MonoBehaviour
{
    public static PersistentAudioManager Instance;
    public AudioMixer mixer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

         
            float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

            mixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);
            mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVol) * 20);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
