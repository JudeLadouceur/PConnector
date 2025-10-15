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

        int target = 0;

        for (int i = 0; i < currentCall.connections.Length; i++)
        {
            if (currentCall.connections[i].receiver == receiver)
            {
                target = i;
            }
        }
        

        dm.StartDialogue(currentCall.connections[target].dialogue);
    }


}
