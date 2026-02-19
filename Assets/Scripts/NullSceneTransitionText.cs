using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NullSceneTransitionText : MonoBehaviour
{
    public TextMeshProUGUI errorScene;
    public TextMeshProUGUI errorVariables;

    private List<DialogueVar> variablesBeingChecked = new List<DialogueVar>();

    private SceneManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = SceneManager.instance;

        string sceneName = (sm.GetNode(sm.currentSceneID) as SceneNode).sceneName;

        WriteText(errorScene, $"{sceneName}, day: {TimeManager.dayNumber} couldn't find a valid connection");

        WriteText(errorVariables, "Variables being checked and their current value: ");

        GetAllVariablesBeingCheckedInConnections();

        foreach (DialogueVar var in variablesBeingChecked)
        {
            WriteText(errorVariables, $"{var}: {VariableManager.instance.flags[var]}, ");
        }
    }

    public void WriteText(TextMeshProUGUI targetTextBox, string errorText)
    {
        Debug.Log(errorText);
        targetTextBox.text += errorText;
    }

    public void GetAllVariablesBeingCheckedInConnections()
    {
        CodeGraphNode node = null;

        bool goToNextConnection = true;

        int i = -1;

        while (true)
        {
            //If the connection being checked is a dead end...
            if (goToNextConnection)
            {
                //Check the next connection
                i++;
                node = sm.GetNodeFromOutput(sm.currentSceneID, 0, i);
            }
            //Otherwise, keep heading down this connection
            else goToNextConnection = true;

            //If all connections were run through and no valid scene was found...
            if (node == null) break;

            node.SetUniqueVariables();

            if (node.GetNodeType() == "Check Variable")
            {
                VariableCheckNode vNode = node as VariableCheckNode;

                foreach (VariableCheckNode.VariableCheck var in vNode.Variables)
                {
                    if(!variablesBeingChecked.Contains(var.variableName)) variablesBeingChecked.Add(var.variableName);
                }

                if (vNode.CheckIfValid())
                {
                    node = sm.GetNodeFromOutput(node.id, 0, 0);
                    goToNextConnection = false;
                }
                else goToNextConnection = true;
            }
            else if (node.continueAfterProcess)
            {
                Debug.Log("Processing node: " + node + ". ID: " + node.id);

                node.OnProcess();
                node = sm.GetNodeFromOutput(node.id, 0, 0);

                Debug.Log("Then going to: " + node + ". ID: " + node.id);

                goToNextConnection = false;
            }
            else break;
        }
    }
}
