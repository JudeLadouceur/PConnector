using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            public Connections[] connections;
        }

        public Call[] call;
    }

    public Days[] days;
}
