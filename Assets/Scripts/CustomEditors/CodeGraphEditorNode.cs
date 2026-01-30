using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CodeGraphEditorNode : Node
{
    private CodeGraphNode m_graphNode;
    public CodeGraphEditorNode(CodeGraphNode node)
    {
        this.AddToClassList("code-graph-node");

        m_graphNode = node;

        Type typeInfo = node.GetType();
        NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

        title = info.title;

        string[] depths = info.menuItem.Split('/');
        foreach (string depth in depths)
        {
            this.AddToClassList(depth.ToLower().Replace(' ', '-'));
        }

        this.name = typeInfo.Name;
    }


}
