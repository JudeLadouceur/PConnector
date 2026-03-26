using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalAppear : MonoBehaviour
{
    [Serializable]
    public class VariableInfo
    {
        public DialogueVar variable;
        public int value;
    }

    public VariableInfo[] Variables;

    private void Awake()
    {
        VariableInfo variableInfo = null;

        for (int i = 0; i < Variables.Length; i++)
        {
            Debug.Log("Checking option: " + i);

            variableInfo = Variables[i];

            //If the variable is incorrect stop checking and disappear
            if (VariableManager.instance.flags[variableInfo.variable] != variableInfo.value)
            {
                Debug.Log("variableInfo " + variableInfo.variable + " was " + VariableManager.instance.flags[variableInfo.variable] + ", but was looking for " + variableInfo.value);

                gameObject.SetActive(false);

                return;
            }
            //If the variable is correct, keep going
            else
            {
                Debug.Log("variableInfo " + variableInfo.variable + " is the correct value.");
            }
        }
    }
}
