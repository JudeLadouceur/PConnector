using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TutorialCallManager : MonoBehaviour
{

    [System.Serializable]
    public class Call
    {

        [System.Serializable]
        public class Connections
        {
            [System.Serializable]
            public class Option
            {
                public SO_Dialogue dialogue;
                public bool doNotProgressToNextCall;
            }

            public Characters receiver;
            public Option[] dialogueOptions;
        }

        [System.Serializable]
        public class ContextCall
        {
            public SO_Dialogue contextCall;
        }
        public Characters caller;

        public ContextCall[] contextCalls;

        public Connections[] connections;
    }

    public Call call;

    private bool callDone = false;


    public static CallManager instance;

    [HideInInspector]
    public bool inContextCall;

    public GameObject telephoneRingingSprite;

    private void Start()
    {
        if (FindAnyObjectByType<ForceAssignNotch>().isActive) LineManager.instance.SelectPoint(FindAnyObjectByType<ForceAssignNotch>().autoNotches[0].transform.GetChild(1).gameObject);

        StartCoroutine(StartCallDelay());
    }

    public void ContextCall()
    {
        if (call.contextCalls.Length == 0)
        {
            Debug.LogError("There are no context calls in day " + TimeManager.dayNumber + ", call " + TimeManager.callNumber + ". Please assign a context call.");
            if (callDone) DialogueManager.Instance.EndDay();
            else ContextCall();
            return;
        }

        SO_Dialogue contextCall = null;

        foreach (Call.ContextCall call in call.contextCalls)
        {
            if (contextCall != null) break;

            contextCall = call.contextCall;
            break;
        }

        inContextCall = true;

        DialogueManager.Instance.StartDialogue(contextCall, false);
    }

    public bool StartCall(Characters receiver)
    {
        bool doNotProgress = false;

        Call currentCall = call;

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


        for (int i = 0; i < currentCall.connections[target].dialogueOptions.Length; i++)
        {
            Debug.Log("Checking option: " + i);

            dialogue = currentCall.connections[target].dialogueOptions[i].dialogue;
            doNotProgress = currentCall.connections[target].dialogueOptions[i].doNotProgressToNextCall;

            Debug.Log("No variable checks, playing: " + currentCall.connections[target].dialogueOptions[i].dialogue);

            if (i + 1 < currentCall.connections[target].dialogueOptions.Length) Debug.LogWarning("Unreachable dialogue detected. Dialogue with no requirements is placed above other dialogue possibilities, making them unreachable.");
            break;

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

        DialogueManager.Instance.StartDialogue(dialogue, doNotProgress);
        return true;
    }

    public IEnumerator StartCallDelay()
    {
        //------------Insert ring noise here----------------
        if (FMODSoundPlayer.Instance != null)
            FMODSoundPlayer.Instance.PlayFMODSound(2);

        telephoneRingingSprite.SetActive(true);

        yield return new WaitForSeconds(4f);

        telephoneRingingSprite.SetActive(false);

        ContextCall();
    }
}
