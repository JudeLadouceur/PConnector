#if(UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[CustomEditor(typeof(SceneManager))]
public class CodeGraphAssetEditor : Editor
{
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int index) 
    {
        Object asset = EditorUtility.InstanceIDToObject(instanceId);
        if (asset.GetType() == typeof(SceneManager))
        {
            CodeGraphEditorWindow.Open((SceneManager)asset);
            return true;
        }
        return false;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open"))
        {
            CodeGraphEditorWindow.Open((SceneManager)target);
        }
    }
}
#endif