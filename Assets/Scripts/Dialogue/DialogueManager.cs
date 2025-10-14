using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManagaer : MonoBehaviour
{
    private GameObject dialogueBox;
    
    void Start()
    {
        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        dialogueBox.SetActive(false);
    }

    public void StartDialogue()
    {

    }
}
