using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    //public AudioMixer mixer;

    // These variables match the names of the ones attached to the Audio Mixer.
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Slider voicesSlider;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus voicesBus;

    void Start()
    {
        // Access the audio mixers set in FMOD.
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        voicesBus = RuntimeManager.GetBus("bus:/Voices");

        // Set them up with PlayerPrefs.
        float master = PlayerPrefs.GetFloat("masterVolume", 1f);
        float music = PlayerPrefs.GetFloat("musicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("sfxVolume", 1f);
        float voices = PlayerPrefs.GetFloat("voicesVolume", 1f);

        // Set the sliders equal to the PlayerPrefs variables, so they get saved.
        masterSlider.value = master;
        musicSlider.value = music;
        SFXSlider.value = sfx;
        voicesSlider.value = voices;

        // Set the FMOD audio mixers equal to the sliders' values.
        masterBus.setVolume(masterSlider.value);
        musicBus.setVolume(musicSlider.value);
        sfxBus.setVolume(SFXSlider.value);
        voicesBus.setVolume(voicesSlider.value);

        // Setting the buses in the Inspector caused sync issues. So, the sliders are instead being synced in code here.
        masterSlider.onValueChanged.AddListener(UpdateMasterVolume);
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        SFXSlider.onValueChanged.AddListener(UpdateSFXVolume);
        voicesSlider.onValueChanged.AddListener(UpdateVoicesVolume);

        /*
        // Do we have saved volume player prefs?
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            // Set the mixer volume levels based on the saved player prefs.
            masterBus.setVolume(PlayerPrefs.GetFloat("masterVolume"));
            musicBus.setVolume(PlayerPrefs.GetFloat("musicVolume"));
            sfxBus.setVolume(PlayerPrefs.GetFloat("SFXVolume"));
            voicesBus.setVolume(PlayerPrefs.GetFloat("VoicesVolume"));

            SetSliders();
        }
        // Otherwise just set the sliders.
        else
        {
            SetSliders();
        }
        */
    }

    void SetSliders()
    {
        /*
        mixer.GetFloat("masterVolume", out float master);
        masterSlider.value = Mathf.Pow(10,(master/20));

        mixer.GetFloat("musicVolume", out float music);
        musicSlider.value = Mathf.Pow(10, (music / 20));

        mixer.GetFloat("SFXVolume", out float sfx);
        SFXSlider.value = Mathf.Pow(10, (sfx / 20));
        */
    }

    // For updating the master slider.
    public void UpdateMasterVolume(float value)
    {
        //mixer.SetFloat("masterVolume", Mathf.Log10(masterSlider.value) * 20);

        masterBus.setVolume(value);

        PlayerPrefs.SetFloat("masterVolume", value);
    }

    // For updating the music slider.
    public void UpdateMusicVolume(float value)
    {
        //mixer.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20);

        musicBus.setVolume(value);

        PlayerPrefs.SetFloat("musicVolume", value);
    }

    // For updating the SFX slider.
    public void UpdateSFXVolume(float value)
    {
        //mixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);

        sfxBus.setVolume(value);

        PlayerPrefs.SetFloat("sfxVolume", value);
    }

    // For updating the Voices slider.
    public void UpdateVoicesVolume(float value)
    {
        //mixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);

        voicesBus.setVolume(value);

        PlayerPrefs.SetFloat("voicesVolume", value);
    }
}