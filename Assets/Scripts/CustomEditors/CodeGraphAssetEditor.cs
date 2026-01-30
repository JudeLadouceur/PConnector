using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CodeGraphAsset))]
public class CodeGraphAssetEditor : Editor
{

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open"))
        {
            CodeGraphEditorWindow.Open((CodeGraphAsset)target);
        }
    }
}
