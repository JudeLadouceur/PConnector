using System.Collections;
using System.Collections.Generic;
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

    public CodeGraphAsset()
    {
        m_nodes = new List<CodeGraphNode>();
        m_connections = new List<CodeGraphConnection>();
    }
}
