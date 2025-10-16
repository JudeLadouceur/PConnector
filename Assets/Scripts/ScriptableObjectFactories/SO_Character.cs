using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character", order = 2)]
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
