using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CodeGraphEditorNode : Node
{
    private CodeGraphNode m_graphNode;

    private Port m_outputPort;
    private List<Port> m_ports;

    public CodeGraphNode Node => m_graphNode;
    public List<Port> Ports => m_ports;

    public CodeGraphEditorNode(CodeGraphNode node)
    {
        this.AddToClassList("code-graph-node");

        m_graphNode = node;

        Type typeInfo = node.GetType();
        NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

        title = info.title;

        m_ports = new List<Port>();

        string[] depths = info.menuItem.Split('/');
        foreach (string depth in depths)
        {
            this.AddToClassList(depth.ToLower().Replace(' ', '-'));
        }

        this.name = typeInfo.Name;

        if (info.hasFlowInput)
        {
            CreateFlowInputPort();
        }

        if (info.hasFlowOutput)
        {
            CreateFlowOutputPort();
        }
    }

    private void CreateFlowInputPort()
    {
        Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(PortTypes.FlowPort));
        inputPort.portName = "Input";
        inputPort.tooltip = "The flow input";
        m_ports.Add(inputPort);
        inputContainer.Add(inputPort);
    }

    private void CreateFlowOutputPort()
    {
        m_outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(PortTypes.FlowPort));
        m_outputPort.portName = "Out";
        m_outputPort.tooltip = "The flow output";
        m_ports.Add(m_outputPort);
        outputContainer.Add(m_outputPort);
    }



    public void SavePosition()
    {
        m_graphNode.SetPosition(GetPosition());
    }
}
