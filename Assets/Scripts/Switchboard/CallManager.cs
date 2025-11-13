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
            public class Connections
            {
                public SO_Character receiver;
                public SO_Dialogue dialogue;
            }

            public SO_Character caller;

            public SO_Dialogue contextCall;

            public Connections[] connections;
        }

        public Call[] call;
    }

    public Days[] days;

    public bool inContextCall;

    private void Start()
    {
        if (FindAnyObjectByType<ForceAssignNotch>().isActive) LineManager.instance.SelectPoint(FindAnyObjectByType<ForceAssignNotch>().autoNotches[0].transform.GetChild(1).gameObject);

        ContextCall();
    }

    public void ContextCall()
    {
        inContextCall = true;

        DialogueManager.Instance.StartDialogue(days[TimeManager.dayNumber].call[TimeManager.callNumber].contextCall);
    }

    public void StartCall(SO_Character receiver)
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

        if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.TheFirstConnection, out Achievement value) && value.status == AchievementStatus.Revealed)
        {
            value.Achieve();
        }

        if (target != -1) DialogueManager.Instance.StartDialogue(currentCall.connections[target].dialogue);
        else Debug.LogError("There is no dialogue assigned to that receiver. Assign someone by opening the dialogueCanvas -> Call manager, and opening days -> call -> caller and assigning a dialogue prefab to the dialogue field. (instructions for creating a dialogue ScriptableObject is in Assets -> Dialogue)");
    }


}
