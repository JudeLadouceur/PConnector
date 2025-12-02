using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
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
        public string dialogue;
    }

    public Lines[] lines;

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
}
