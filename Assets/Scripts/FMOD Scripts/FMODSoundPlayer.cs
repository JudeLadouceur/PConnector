using FMODUnity;
using UnityEngine;

public class FMODSoundPlayer : MonoBehaviour
{
    public static FMODSoundPlayer Instance; // Making a Singleton, so it can be called from anywhere.

    public EventReference[] soundEvents;

    private void Awake()
    {
        Instance = this;
    }

    // This function is used for simple FMOD lines that don't need any parameters. Things like the call prompts.
    public void PlayFMODSound(int index)
    {
        if (index < 0 || index >= soundEvents.Length)
        {
            Debug.LogWarning("FMODSoundPlayer has an invalid event index.");

            return;
        }

        RuntimeManager.PlayOneShot(soundEvents[index]);
    }
}