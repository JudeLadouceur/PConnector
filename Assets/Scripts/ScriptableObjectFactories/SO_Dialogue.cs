using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum VarType
{
    Int, Bool
};

public enum Modifier
{
    Addition, Substraction, Equal
};

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Dialogue Asset", order = 1)]
public class SO_Dialogue : ScriptableObject
{
    [System.Serializable]

    public class Lines
    {
        public Characters NewSpeakerName;
        public string dialogue;
    }

    public Lines[] lines;

    public string contextSummary;

    public bool hasNoAudio = false;

    public bool isBark = true;

    public EventReference voiceLineEvent; // FMOD Event for character voice lines.

    public EventReference barkEvent; // FMOD Event for character barks.

    [System.Serializable]
    public class Variables
    {
        public DialogueVar variable;
        public Modifier modifier;
        public int value;
    }
    public Variables[] variables;

    void Awake()
    {
        if (Application.isPlaying)
        {
            if (hasNoAudio) return;
            if (isBark)
            {
                if (!barkEvent.IsNull) return;
                Debug.Log("Added placeholder bark to " + name);
                barkEvent = FMODUnity.RuntimeManager.PathToEventReference("event:/Voices/Placeholder Voice Lines/Bark Placeholder Voice Line");
            }
            else
            {
                if (!voiceLineEvent.IsNull) return;
                Debug.Log("Added placeholder voice line to " + name);
                voiceLineEvent = FMODUnity.RuntimeManager.PathToEventReference("event:/Voices/Placeholder Voice Lines/Placeholder Voice Line (Switchboard)");
            }
        }
    }
}
