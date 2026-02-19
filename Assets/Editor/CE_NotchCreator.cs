using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(NotchCreator))]
class CE_NotchCreator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Build Notches"))
            target.GetComponent<NotchCreator>().BuildNotches();
    }
}
