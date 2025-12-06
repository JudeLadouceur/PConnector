using FMODUnity;
using UnityEngine;

public class FMODSoundPlayer : MonoBehaviour
{
    public static FMODSoundPlayer Instance; // Making a Singleton, so it can be called from anywhere.

    public EventReference FMODSoundEvent; // FMOD Event for sounds.

    private void Awake()
    {
        Instance = this;
    }

    // This function is used for simple FMOD lines that don't need any parameters. Things like the call prompts.
    public void PlayFMODSound()
    {
       RuntimeManager.PlayOneShot(FMODSoundEvent);
    }
}