using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

// This script is responsible for acting as a bridge between FMOD and Unity for making our voice lines play.
public class DialogueVoiceManager : MonoBehaviour
{
    public static DialogueVoiceManager Instance; // Making a Singleton, so it can be called from anywhere.

    private void Awake()
    {
        Instance = this;
    }

    // This function is used for simple FMOD lines that don't need any parameters. Things like the call prompts.
    //public void PlayVoiceLine(EventReference eventRef)
    //{
        //RuntimeManager.PlayOneShot(eventRef);
    //}

    // This function is used for FMOD lines that do need to use paramaters to play. Things like the the dialogue choices and long dialogue lines.
    public EventInstance PlayVoiceLine(EventReference eventRef, int chunkIndex)
    {
        //if (dialogue.lines[D].voiceLineEvent)
        //{
            //Debug.Log("No FMOD voice event was found on this dialogue line.");
            //return;
        //}

        EventInstance instance = RuntimeManager.CreateInstance(eventRef);

        instance.setParameterByName("dialogueProgress", chunkIndex);
        instance.start();
        instance.release();
        return instance;
    }
}