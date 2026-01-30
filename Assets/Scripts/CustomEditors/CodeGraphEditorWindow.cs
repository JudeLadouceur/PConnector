using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CodeGraphEditorWindow : EditorWindow
{
    public static void Open(CodeGraphAsset target)
    {
        CodeGraphEditorWindow[] windows = Resources.FindObjectsOfTypeAll<CodeGraphEditorWindow>();
        foreach (var w in windows)
        {
            if(w.m_currentGraph = target)
            {
                w.Focus();
                return;
            }
        }

        CodeGraphEditorWindow window = CreateWindow<CodeGraphEditorWindow>(typeof(CodeGraphEditorWindow), typeof(SceneView));
        window.titleContent = new GUIContent($"{target.name}", EditorGUIUtility.ObjectContent(null, typeof(CodeGraphAsset)).image);
        window.Load(target);
    }

    [SerializeField]
    private CodeGraphAsset m_currentGraph;

    [SerializeField]
    private SerializedObject m_serializedObject;

    [SerializeField]
    private CodeGraphView m_currentView;

    public CodeGraphAsset sceneManager => m_currentGraph;

    private void Load(CodeGraphAsset target)
    {
        m_currentGraph = target;
        DrawGraph();
    }
    private void DrawGraph()
    {
        m_serializedObject = new SerializedObject(m_currentGraph);
        m_currentView = new CodeGraphView(m_serializedObject);
        rootVisualElement.Add(m_currentView);
    }
}
