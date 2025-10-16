using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
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

    [System.Serializable]
    public class Variables
    {
        public SO_Character targetCharacter;
        public string variableName;
        public Modifier modifier;
        public int value;
    }
    public Variables[] variables;
}
