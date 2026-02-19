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
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

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

    // This function is used for FMOD lines that do need to use paramaters to play. Things like the the dialogue choices and long dialogue lines.
    public EventInstance PlayBark(EventReference eventRef)
    {
        //if (dialogue.lines[D].voiceLineEvent)
        //{
        //Debug.Log("No FMOD voice event was found on this dialogue line.");
        //return;
        //}

        EventInstance instance = RuntimeManager.CreateInstance(eventRef);

        instance.setParameterByName("dialogueProgress", 0);
        instance.start();
        instance.release();
        return instance;
    }
}