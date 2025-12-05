using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CallManager : MonoBehaviour
{
    [System.Serializable]
    public class Days
    {
        [System.Serializable]
        public class Call
        {
            [System.Serializable]
            public class RequiredVars
            {
                public DialogueVar variableName;
                public int value;
            }

            [System.Serializable]
            public class Connections
            {
                [System.Serializable]
                public class Option
                {
                    public RequiredVars[] variables;
                    public SO_Dialogue dialogue;
                }
                
                public Characters receiver;
                public Option[] dialogueOptions;
            }

            public RequiredVars[] requiredVars;

            public Characters caller;

            public SO_Dialogue contextCall;

            public Connections[] connections;
        }

        public Call[] call;
    }

    public Days[] days;

    public static CallManager instance;

    [HideInInspector]
    public bool inContextCall;

    private void Start()
    {
        if (FindAnyObjectByType<ForceAssignNotch>().isActive) LineManager.instance.SelectPoint(FindAnyObjectByType<ForceAssignNotch>().autoNotches[0].transform.GetChild(1).gameObject);

        StartCoroutine(StartCallDelay());
    }

    public void ContextCall()
    {
        foreach (Days.Call.RequiredVars var in days[TimeManager.dayNumber].call[TimeManager.callNumber].requiredVars)
        {
            if (var.value != VariableManager.instance.flags[var.variableName])
            {
                TimeManager.callNumber++;
                if (TimeManager.callNumber == days[TimeManager.dayNumber].call.Length) DialogueManager.Instance.EndDay();
                else ContextCall();
                return;
            }
        }

        inContextCall = true;

        if(days[TimeManager.dayNumber].call[TimeManager.callNumber].contextCall == null)
        {
            Debug.LogError("There is no context call for day " + TimeManager.dayNumber + ", call " + TimeManager.callNumber + ". Please provide a context call.");
            return;
        }

        DialogueManager.Instance.StartDialogue(days[TimeManager.dayNumber].call[TimeManager.callNumber].contextCall);
    }

    public bool StartCall(Characters receiver)
    {
        Days.Call currentCall = days[TimeManager.dayNumber].call[TimeManager.callNumber];

        int target = -1;

        for (int i = 0; i < currentCall.connections.Length; i++)
        {
            if (currentCall.connections[i].receiver == receiver)
            {
                target = i;
                break;
            }
        }

        if (target == -1)
        {
            Debug.LogError("There is no dialogue assigned to that receiver. Assign someone by opening the dialogueCanvas -> Call manager, and opening days -> call -> caller and assigning a dialogue prefab to the dialogue field. (instructions for creating a dialogue ScriptableObject is in Assets -> Dialogue)");
            return false;
        }

        SO_Dialogue dialogue = null;

        Days.Call.RequiredVars variable = null;


        for (int i = 0; i < currentCall.connections[target].dialogueOptions.Length; i++)
        {
            Debug.Log("Checking option: " + i);
            Debug.Log("Number of variable checks: " + currentCall.connections[target].dialogueOptions[i].variables.Length);

            if (currentCall.connections[target].dialogueOptions[i].variables.Length == 0)
            {
                dialogue = currentCall.connections[target].dialogueOptions[i].dialogue;
                Debug.Log("No variable checks, playing: " + currentCall.connections[target].dialogueOptions[i].dialogue);

                if (i + 1 < currentCall.connections[target].dialogueOptions.Length) Debug.LogWarning("Unreachable dialogue detected. Dialogue with no requirements is placed above other dialogue possibilities, making them unreachable.");
                break;
            }
            
            for (int o = 0; o < currentCall.connections[target].dialogueOptions[i].variables.Length; o++)
            {
                variable = currentCall.connections[target].dialogueOptions[i].variables[o];
                Debug.Log("Checking: " + variable.variableName);

                if (VariableManager.instance.flags[variable.variableName] == variable.value)
                {
                    Debug.Log("Variable " + variable.variableName + " is the correct value.");
                    dialogue = currentCall.connections[target].dialogueOptions[i].dialogue;
                    break;
                }
                Debug.Log("Variable " + variable.variableName + " was " + variable.value + ", but was looking for " + VariableManager.instance.flags[variable.variableName]);
            }

            if (dialogue != null) break;
        }

        /*if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.TheFirstConnection, out Achievement value) && value.status == AchievementStatus.Revealed)
        {
            value.Achieve();
        }*/

        if (dialogue == null)
        {
            Debug.LogError("There is no valid dialogue to play in this notch. Please ensure that a dialogue can play with the variable values you currently have (or have a dialogue with no variable requiremnets).");
            return false;
        }

        DialogueManager.Instance.StartDialogue(dialogue);
        return true;
    }

    public IEnumerator StartCallDelay()
    {
        //------------Insert ring noise here----------------
        
        yield return new WaitForSeconds(2f);

        ContextCall();
    }
}
