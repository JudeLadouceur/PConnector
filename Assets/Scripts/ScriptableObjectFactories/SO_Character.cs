using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SO_Character : ScriptableObject
{

    public string characterName;
    public Heuristics[] variables;

    [System.Serializable]
    public class Heuristics 
    {
        public string name;
        public int value;

    }

}
