using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class FMODSoundPlayer : MonoBehaviour
{
    public static FMODSoundPlayer Instance; // Making a Singleton, so it can be called from anywhere.

    public EventReference[] soundEvents;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // This function is used for one shot FMOD sounds that can be played from a custom array. 
    public EventInstance PlayFMODSound(int index)
    {
        EventInstance instance = RuntimeManager.CreateInstance(soundEvents[index]);

        instance.setParameterByName("dialogueProgress", 0);
        instance.start();
        instance.release();
        return instance;
    }
}