using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


[CustomEditor(typeof(SO_Dialogue))]
[CanEditMultipleObjects]
class SO_DialogueCE : Editor
{
    SerializedProperty lines;

    SerializedProperty speakerName;
    SerializedProperty speakerProfile;
    SerializedProperty dialogue;

    bool isMonologue = false;

    private void OnEnable()
    {
        lines = serializedObject.FindProperty("lines");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        isMonologue = EditorGUILayout.Toggle("Is this a monologue?", isMonologue);

        if (isMonologue)
        {
            EditorGUILayout.PropertyField(lines);
            //EditorGUILayout.PropertyField(speakerProfile);
            //EditorGUILayout.PropertyField(dialogue);
        }

        serializedObject.ApplyModifiedProperties();
    }
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Dialogue Asset", order = 1)]
public class SO_Dialogue : ScriptableObject
{

    

    [System.Serializable]
    public class Lines
    {
        public string speakerName;
        public Image speakerProfile;
        public string dialogue;
    }

    public List<Lines> lines;
}
