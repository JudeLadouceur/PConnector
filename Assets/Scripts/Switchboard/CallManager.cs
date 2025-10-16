using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallManager : MonoBehaviour
{
    private DialogueManager dm;

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

            public Connections[] connections;
        }

        public Call[] call;
    }

    public Days[] days;

    private void Start()
    {
        dm = GameObject.FindAnyObjectByType<DialogueManager>();
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

        //-------------Insert achievement code here---------------------

        if (target != -1) dm.StartDialogue(currentCall.connections[target].dialogue);
        else Debug.LogError("There is no dialogue assigned to that receiver. Assign someone by opening the dialogueCanvas -> Call manager, and opening days -> call -> caller and assigning a dialogue prefab to the dialogue field. (instructions for creating a dialogue ScriptableObject is in Assets -> Dialogue)");
    }


}
