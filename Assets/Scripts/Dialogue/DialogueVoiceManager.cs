using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class DialogueVoiceManager : MonoBehaviour
{
    public static DialogueVoiceManager Instance; // Making a Singleton, so it can be called from anywhere.

    private void Awake()
    {
        Instance = this;
    }

    // This function is used for simple FMOD lines that don't need any parameters. Things like the call prompts.
    public void PlayVoiceLine(EventReference eventRef)
    {
        RuntimeManager.PlayOneShot(eventRef);
    }

    // This function is used for FMOD lines that do need to use paramaters to play. Things like the the dialogue choices and long dialogue lines.
    public EventInstance PlayVoiceLineWithParameter(EventReference eventRef, string parameter, float value)
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventRef);
        instance.setParameterByName(parameter, value);
        instance.start();
        instance.release();
        return instance;
    }
}