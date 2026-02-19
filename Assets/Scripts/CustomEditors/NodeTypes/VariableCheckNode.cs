using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeInfo("Check Variable", "Variables/Check Variable")]
public class VariableCheckNode : CodeGraphNode
{
    [Serializable]
    public class VariableCheck 
    {
        public DialogueVar variableName;
        public int value;
    }
    
    [ExposedProperty()]
    public List<VariableCheck> Variables = new List<VariableCheck>();

    public bool CheckIfValid()
    {
        foreach (VariableCheck v in Variables) 
        {
            Debug.Log("Checking " +  v.variableName);
            if (VariableManager.instance.flags[v.variableName] != v.value)
            {
                Debug.Log("Value of " + v.variableName + " wasn't met (current value: " + VariableManager.instance.flags[v.variableName] + ". Expected value: " + v.value);
                return false;
            }
            Debug.Log("Value of " + v.variableName + " was met (current value: " + VariableManager.instance.flags[v.variableName] + ". Expected value: " + v.value);
        }
        return true;
    }

    public override void SetUniqueVariables()
    {
        m_continueAfterProcess = true;
    }
}
