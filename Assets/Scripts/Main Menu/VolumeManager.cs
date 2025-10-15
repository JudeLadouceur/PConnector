using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

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
            mixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume"));
            mixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
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
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    // For updating the master slider.
    public void UpdateMasterVolume()
    {
        mixer.SetFloat("masterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
    }

    // For updating the music slider.
    public void UpdateMusicVolume()
    {
        mixer.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    // For updating the SFX slider.
    public void UpdateSFXVolume()
    {
        mixer.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }
}