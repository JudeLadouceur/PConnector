using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeInfo("Set Variable", "Variables/Set Variable")]
public class VariableSetNode : CodeGraphNode
{
    [Serializable]
    public class VariableCheck
    {
        public DialogueVar variableName;
        public int value;
    }

    [ExposedProperty()]
    public List<VariableCheck> Variables = new List<VariableCheck>();

    public override void OnProcess()
    {
        foreach (VariableCheck v in Variables)
        {
            VariableManager.instance.flags[v.variableName] = v.value;
            Debug.Log("Set " +  v.variableName + " to: " + v.value);
        }
    }

    public override void SetUniqueVariables()
    {
        m_continueAfterProcess = true;
    }
}
