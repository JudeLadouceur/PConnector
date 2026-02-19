#if(UNITY_EDITOR)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CodeGraphEditorWindow : EditorWindow
{
    public static void Open(SceneManager target)
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
        window.titleContent = new GUIContent($"{target.name}", EditorGUIUtility.ObjectContent(null, typeof(SceneManager)).image);
        window.Load(target);
    }

    [SerializeField]
    private SceneManager m_currentGraph;

    [SerializeField]
    private SerializedObject m_serializedObject;

    [SerializeField]
    private CodeGraphView m_currentView;

    public SceneManager sceneManager => m_currentGraph;

    private void OnEnable()
    {
        if (m_currentGraph != null) DrawGraph();
    }

    private void OnGUI()
    {
        if (m_currentGraph != null)
        {
            if (EditorUtility.IsDirty(m_currentGraph)) this.hasUnsavedChanges = true;

            else this.hasUnsavedChanges = false;
        }
    }

    private void Load(SceneManager target)
    {
        m_currentGraph = target;
        DrawGraph();
    }
    private void DrawGraph()
    {
        m_serializedObject = new SerializedObject(m_currentGraph);
        m_currentView = new CodeGraphView(m_serializedObject, this);
        m_currentView.graphViewChanged += OnChange;
        rootVisualElement.Add(m_currentView);
    }

    private GraphViewChange OnChange(GraphViewChange graphViewChange)
    {
        
        EditorUtility.SetDirty(m_currentGraph);
        return graphViewChange;
    }
}
#endif