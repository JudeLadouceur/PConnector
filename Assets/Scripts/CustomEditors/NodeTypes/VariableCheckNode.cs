using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeInfo("Variable Check", "Variables/Variable Check")]
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
            if (VariableManager.instance.flags[v.variableName] != v.value) return false;
        }
        return true;
    }
}
