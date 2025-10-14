using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

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
}
