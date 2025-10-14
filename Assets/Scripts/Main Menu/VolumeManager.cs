using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// This tutorial was used for writing this script:
// https://gamedevacademy.org/unity-audio-settings-tutorial/

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mixer;

    // These variables match the names of the ones attached to the Audio Mixer.
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;

    void Start()
    {
        // Do we have saved volume player prefs?
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            // Set the mixer volume levels based on the saved player prefs.
            mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
   
            SetSliders();
        }
        // Otherwise just set the sliders.
        else
        {
            SetSliders();
        }
    }

    void SetSliders()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    // For updating the master slider.
    public void UpdateMasterVolume()
    {
        mixer.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    // For updating the music slider.
    public void UpdateMusicVolume()
    {
        mixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    // For updating the SFX slider.
    public void UpdateSFXVolume()
    {
        mixer.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }
}