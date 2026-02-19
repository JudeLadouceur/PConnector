using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class CodeGraphNode: ISerializationCallbackReceiver
{
    [SerializeField]
    private string m_guid;
    [SerializeField]
    private Rect m_position;

    public string typeName;

    protected bool m_continueAfterProcess = false;

    public bool continueAfterProcess => m_continueAfterProcess;

    public string id => m_guid;
    public Rect position => m_position;

    public CodeGraphNode()
    {
        NewGUID();
    }

    private void NewGUID()
    {
        m_guid = Guid.NewGuid().ToString();
    }

    public void SetPosition(Rect position)
    {
        m_position = position;
    }

    public virtual string GetNodeType()
    {
        Type typeInfo = GetType();
        NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

        return info.title;
    }

    public virtual void OnProcess()
    {
        
    }

    public virtual void SetUniqueVariables()
    {
        
    }

    public virtual void OnBeforeSerialize()
    {
    }

    public virtual void OnAfterDeserialize()
    {
    }
}
