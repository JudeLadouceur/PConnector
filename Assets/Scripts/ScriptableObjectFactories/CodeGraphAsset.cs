using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scene Manager/New Manager")]
public class CodeGraphAsset : ScriptableObject
{
    [SerializeReference]
    private List<CodeGraphNode> m_nodes;
    [SerializeField]
    private List<CodeGraphConnection> m_connections;

    public List<CodeGraphNode> Nodes => m_nodes;
    public List<CodeGraphConnection> Connections => m_connections;

    private Dictionary<string, CodeGraphNode> m_nodeDictionary;

    public GameObject gameObject;

    public CodeGraphAsset()
    {
        m_nodes = new List<CodeGraphNode>();
        m_connections = new List<CodeGraphConnection>();
    }

    public void Init(GameObject gameObject)
    {
        this.gameObject = gameObject;

        m_nodeDictionary = new Dictionary<string, CodeGraphNode>();

        foreach (CodeGraphNode node in Nodes)
        {
            m_nodeDictionary.Add(node.id, node);
        }
    }

    public CodeGraphNode GetStartNode()
    {
        StartNode[] startNodes = Nodes.OfType<StartNode>().ToArray();

        if(startNodes.Length == 0)
        {
            Debug.LogError("There is no start node in this code graph.");
            return null;
        }

        return startNodes[0];
    }

    public CodeGraphNode GetNode(string nextNodeId)
    {
        if(m_nodeDictionary.TryGetValue(nextNodeId, out CodeGraphNode node))
        {
            return node;
        }
        return null;
    }

    public CodeGraphNode GetNodeFromOutput(string outputNodeId, int index)
    {
        foreach(CodeGraphConnection connection in m_connections)
        {
            if(connection.outputPort.nodeId == outputNodeId && connection.outputPort.portIndex == index)
            {
                string nodeId = connection.inputPort.nodeId;
                CodeGraphNode inputNode = m_nodeDictionary[nodeId];
                return inputNode;
            }
        }

        return null;
    }
}
