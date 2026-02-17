using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager: MonoBehaviour
{
    public Dictionary<DialogueVar, int> flags;

    public static VariableManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        flags = new Dictionary<DialogueVar, int>();

        for (int i = 0; i < System.Enum.GetValues(typeof(DialogueVar)).Length; i++)
        {
            flags.TryAdd((DialogueVar)i, 0);
        }
    }

    [ContextMenu("Print Variables")]
    public void PrintVariables()
    {
        List<DialogueVar> keys = new List<DialogueVar>(flags.Keys);

        foreach (DialogueVar flag in keys)
        {
            Debug.Log("Variable " + flag + ". value: " + flags[flag]);
        }
    }

    public void ResetFlags()
    {
        List<DialogueVar> keys = new List<DialogueVar>(flags.Keys);
        
        foreach (DialogueVar flag in keys)
        {
            flags[flag] = 0;

            Debug.Log("Successfully reset " + flag + " to: " + flags[flag]);
        }
    }
}
