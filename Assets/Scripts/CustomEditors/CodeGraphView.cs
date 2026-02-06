using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class CodeGraphView : GraphView
{
    private CodeGraphAsset m_codeGraph;
    private SerializedObject m_serializedObject;
    private CodeGraphEditorWindow m_window;

    public CodeGraphEditorWindow window => m_window;

    public List<CodeGraphEditorNode> m_graphNodes;
    public Dictionary<string, CodeGraphEditorNode> m_nodeDictionary;

    private CodeGraphWindowSearchProvider m_searchProvider;

    public CodeGraphView(SerializedObject serializedObject, CodeGraphEditorWindow window)
    {
        m_serializedObject = serializedObject;
        m_codeGraph = (CodeGraphAsset)serializedObject.targetObject;
        m_window = window;

        m_graphNodes = new List<CodeGraphEditorNode>();
        m_nodeDictionary = new Dictionary<string, CodeGraphEditorNode>();

        m_searchProvider = ScriptableObject.CreateInstance<CodeGraphWindowSearchProvider>();
        m_searchProvider.graph = this;

        this.nodeCreationRequest = ShowSearchWindow;


        StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/CustomEditors/USS/CodeGraphEditor.uss");
        styleSheets.Add(style);

        GridBackground background = new GridBackground();
        background.name = "Grid";
        Add(background);
        background.SendToBack();

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new ClickSelector());

        DrawNodes();

        graphViewChanged += onGraphViewChangedEvent;
    }

    private GraphViewChange onGraphViewChangedEvent(GraphViewChange graphViewChange)
    {
        if (graphViewChange.movedElements != null)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Moved Graph Element");
            foreach(CodeGraphEditorNode editorNode in graphViewChange.movedElements.OfType<CodeGraphEditorNode>())
            {
                editorNode.SavePosition();
            }


        }
        
        if(graphViewChange.elementsToRemove != null)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Removed Graph Element");
            
            List<CodeGraphEditorNode> nodes = graphViewChange.elementsToRemove.OfType<CodeGraphEditorNode>().ToList();

            if(nodes.Count > 0)
            {

                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    RemoveNode(nodes[i]);
                }
            }

        }
        return graphViewChange;
    }

    private void RemoveNode(CodeGraphEditorNode editorNode)
    {
        m_codeGraph.Nodes.Remove(editorNode.Node);
        m_nodeDictionary.Remove(editorNode.Node.id);
        m_graphNodes.Remove(editorNode);
        m_serializedObject.Update();
    }

    private void DrawNodes()
    {
        foreach (CodeGraphNode node in m_codeGraph.Nodes)
        {
            AddNodeToGraph(node);
        }
    }

    private void ShowSearchWindow(NodeCreationContext obj)
    {
        m_searchProvider.target = (VisualElement)focusController.focusedElement;
        SearchWindow.Open(new SearchWindowContext(obj.screenMousePosition), m_searchProvider);
    }

    public void Add(CodeGraphNode node)
    {
        Undo.RecordObject(m_serializedObject.targetObject, "Added Node");

        m_codeGraph.Nodes.Add(node);

        m_serializedObject.Update();

        AddNodeToGraph(node);
    }

    private void AddNodeToGraph(CodeGraphNode node)
    {
        node.typeName = node.GetType().AssemblyQualifiedName;

        CodeGraphEditorNode editorNode = new CodeGraphEditorNode(node);
        editorNode.SetPosition(node.position);

        m_graphNodes.Add(editorNode);
        m_nodeDictionary.Add(node.id, editorNode);

        AddElement(editorNode);
    }
}
