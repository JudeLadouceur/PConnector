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
                public DialogueVar variable;
                public int value;
            }

            [System.Serializable]
            public class Connections
            {
                public Characters receiver;
                public SO_Dialogue dialogue;
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
            if (var.value != VariableManager.instance.flags[var.variable])
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

    public void StartCall(Characters receiver)
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

        /*if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.TheFirstConnection, out Achievement value) && value.status == AchievementStatus.Revealed)
        {
            value.Achieve();
        }*/

        if (target != -1) DialogueManager.Instance.StartDialogue(currentCall.connections[target].dialogue);
        else Debug.LogError("There is no dialogue assigned to that receiver. Assign someone by opening the dialogueCanvas -> Call manager, and opening days -> call -> caller and assigning a dialogue prefab to the dialogue field. (instructions for creating a dialogue ScriptableObject is in Assets -> Dialogue)");
    }

    public IEnumerator StartCallDelay()
    {
        yield return new WaitForSeconds(2.5f);

        ContextCall();
    }
}
