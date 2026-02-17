using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DaySixCallManager : MonoBehaviour
{
    public SO_Dialogue[] dialogues;
    private DaySixDialogueManager dialogueManager;
    private int callIndex = 0;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DaySixDialogueManager>();
        StartCoroutine(StartCallDelay());
    }

    public void StartCall()
    {
        dialogueManager.StartDialogue(dialogues[callIndex]);
    }

    public IEnumerator StartCallDelay()
    {

        //------------Insert ring noise here----------------
        if(FMODSoundPlayer.Instance!=null)
            FMODSoundPlayer.Instance.PlayFMODSound(2);
        
        yield return new WaitForSeconds(4f);

        StartCall();
    }

    public void EndCall()
    {
        callIndex++;
        if(callIndex>= dialogues.Length)
        {
            //Inserts unlock menu button code here
            return;
        }
        StartCoroutine(StartCallDelay());
    }
}
