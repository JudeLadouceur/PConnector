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
        mixer.GetFloat("masterVolume", out float master);
        masterSlider.value = Mathf.Pow(10,(master/20));
        mixer.GetFloat("musicVolume", out float music);
        musicSlider.value = Mathf.Pow(10, (music / 20));
        mixer.GetFloat("SFXVolume", out float sfx);
        SFXSlider.value = Mathf.Pow(10, (sfx / 20));
    }

    // For updating the master slider.
    public void UpdateMasterVolume()
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(masterSlider.value) * 20);
        PlayerPrefs.SetFloat("masterVolume", Mathf.Log10(masterSlider.value) * 20);
    }

    // For updating the music slider.
    public void UpdateMusicVolume()
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(musicSlider.value)*20);
        PlayerPrefs.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20);
    }

    // For updating the SFX slider.
    public void UpdateSFXVolume()
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);
    }
}