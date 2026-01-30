using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scene Manager/New Manager")]
public class CodeGraphAsset : ScriptableObject
{
    [SerializeReference]
    private List<CodeGraphNode> m_nodes;

    public List<CodeGraphNode> Nodes => m_nodes;

    public CodeGraphAsset()
    {
        m_nodes = new List<CodeGraphNode>();
    }
}
