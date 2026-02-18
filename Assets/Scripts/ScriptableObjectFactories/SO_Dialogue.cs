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
        public string speakerName;
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
    
    [ExecuteInEditMode]
    private void OnValidate()
    {
        Characters[] characters = (Characters[])System.Enum.GetValues(typeof(Characters));
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].speakerName == Characters.None.ToString()) return;
            //Debug.Log("Changing names of: " + this.ToString());
            string oldName = lines[i].speakerName.ToLower().Replace(" ", "");
            foreach (Characters c in characters)
            {
                if (oldName == c.ToString().ToLower())
                {
                    lines[i].NewSpeakerName = c;
                    //Debug.Log("Changed line " + i + " name to " + c.ToString());
                }
            }

            if (oldName == "mrs.perkins")
            {
                lines[i].NewSpeakerName = Characters.Perkins;
                //Debug.Log("Changed line " + i + " name to " + Characters.Perkins.ToString());
            }
            if (oldName == "emergency")
            { 
                lines[i].NewSpeakerName = Characters.Emergency;
                //Debug.Log("Changed line " + i + " name to " + Characters.Perkins.ToString());
            }
        }

    }

    
}
