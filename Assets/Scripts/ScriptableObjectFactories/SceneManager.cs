using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Scene Manager/New Manager")]
public class SceneManager : ScriptableObject
{
    [SerializeReference]
    private List<CodeGraphNode> m_nodes;
    [SerializeField]
    private List<CodeGraphConnection> m_connections;

    public List<CodeGraphNode> Nodes => m_nodes;
    public List<CodeGraphConnection> Connections => m_connections;

    private Dictionary<string, CodeGraphNode> m_nodeDictionary;

    public GameObject gameObject;

    private string m_currentSceneID;
    public string currentSceneID => m_currentSceneID;

    public static SceneManager instance;

    public SceneManager()
    {
        m_nodes = new List<CodeGraphNode>();
        m_connections = new List<CodeGraphConnection>();
    }

    public void Init(GameObject gameObject, CodeGraphNode mainMenuNode)
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        this.gameObject = gameObject;

        m_nodeDictionary = new Dictionary<string, CodeGraphNode>();

        foreach (CodeGraphNode node in Nodes)
        {
            m_nodeDictionary.Add(node.id, node);
        }

        Debug.Log(mainMenuNode.id);

        m_currentSceneID = mainMenuNode.id;

        Debug.Log(m_currentSceneID);
    }

    public CodeGraphNode GetMainMenuNode()
    {
        MainMenuNode[] startNodes = Nodes.OfType<MainMenuNode>().ToArray();

        if(startNodes.Length == 0)
        {
            Debug.LogError("There is no start node in this code graph.");
            return null;
        }

        Debug.Log(startNodes[0]);

        return startNodes[0];
    }

    public void GoToMainMenu()
    {
        VariableManager.instance.ResetFlags();
        GoToScene(GetMainMenuNode().id);
    }

    public CodeGraphNode GetNode(string nextNodeId)
    {
        if(m_nodeDictionary.TryGetValue(nextNodeId, out CodeGraphNode node))
        {
            return node;
        }
        return null;
    }

    public void GoToNextScene()
    {
        Debug.Log("Starting scene: " + m_currentSceneID);

        int i = -1;

        CodeGraphNode node = null;

        bool goToNextConnection = true;

        while (true) 
        {
            Debug.Log("Go to next connection: " + goToNextConnection);
            if (goToNextConnection)
            {
                i++;
                node = GetNodeFromOutput(m_currentSceneID, 0, i);
            }
            else goToNextConnection = true;

            if (node == null)
            {
                Debug.Log(m_currentSceneID);
                SceneNode n = GetNode(m_currentSceneID) as SceneNode;
                Debug.LogError("No valid connection was found. Either the node has no connections or none of the connections met all the variables required to move to it. The current scene is: " + n.scene);
                break;
            }
            node.SetUniqueVariables();
            if (node.GetNodeType() == "Check Variable")
            {
                VariableCheckNode vNode = node as VariableCheckNode;
                if (vNode.CheckIfValid())
                {
                    node = GetNodeFromOutput(node.id, 0, 0);
                    goToNextConnection = false;
                }
                else goToNextConnection = true;
            }
            else if (node.continueAfterProcess)
            {
                Debug.Log("Processing node: " + node + ". ID: " + node.id);

                node.OnProcess();
                node = GetNodeFromOutput(node.id, 0, 0);

                Debug.Log("Then going to: " + node + ". ID: " + node.id);

                goToNextConnection = false;
            }
            else break;
        }

        if(node != null)
        {
            m_currentSceneID = node.id;
            Debug.Log("Processing node " + node.id);
            Debug.Log(node.GetNodeType());
            node.OnProcess();
        }
    }

    public void GoToScene(string nodeID)
    {
        m_currentSceneID = nodeID;
        GetNode(nodeID).OnProcess();
    }

    public CodeGraphNode GetNodeFromOutput(string outputNodeId, int portIndex, int connectionIndex)
    {
        int i = 0;
        
        foreach(CodeGraphConnection connection in m_connections)
        {
            if(connection.outputPort.nodeId == outputNodeId && connection.outputPort.portIndex == portIndex)
            {
                if (i >= connectionIndex) 
                {
                    string nodeId = connection.inputPort.nodeId;
                    CodeGraphNode inputNode = m_nodeDictionary[nodeId];
                    return inputNode;
                }
                i++;
            }
        }

        return null;
    }
}
