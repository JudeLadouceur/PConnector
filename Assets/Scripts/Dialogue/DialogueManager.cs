using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private GameObject dialogueBox;
    private CallManager callManager;
    private DrawLine drawLine;
    private CharacterManager characterManager;
    private SceneTransitionTargets transitionTargets;


    private int lineNumber;
    private bool inDialogue = false;

    private SO_Dialogue currentDialogue;
    private TextMeshProUGUI speakerField;
    private TextMeshProUGUI dialogueField;

    void Start()
    {
        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        callManager = GameObject.FindAnyObjectByType<CallManager>();
        drawLine = GameObject.FindAnyObjectByType<DrawLine>();
        characterManager = GameObject.FindAnyObjectByType<CharacterManager>();
        transitionTargets = GameObject.FindAnyObjectByType<SceneTransitionTargets>();

        speakerField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 2).GetComponent<TextMeshProUGUI>();
        dialogueField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 1).GetComponent<TextMeshProUGUI>();
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (inDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space)) NextLine();
        }
    }

    public void StartDialogue(SO_Dialogue dialogue)
    {
        print("Begin dialogue " + dialogue.name);

        dialogueBox.SetActive(true);

        currentDialogue = dialogue;
        

        lineNumber = 0;
        SetDialogueLine(0);

        for (int i = 0; i < dialogue.variables.Length; i++) 
        {
            characterManager.flags[dialogue.variables[i].variableName] = dialogue.variables[i].value;
        }

        inDialogue = true;
    }

    //If this is not the last line, increment the line number by 1 and play the new line. If it is, end the dialogue
    public void NextLine()
    {
        if (lineNumber == currentDialogue.lines.Length - 1) EndDialogue();
        else
        {
            lineNumber++;
            SetDialogueLine(lineNumber);
        }
    }

    //Start the line indicated by the received number
    public void SetDialogueLine(int line)
    {
        if(line >= currentDialogue.lines.Length)
        {
            Debug.LogError("This line is out of range, this script can only use a line number that is " + (currentDialogue.lines.Length - 1) + " or lower");
        }
        print("Go to line " + line);
        speakerField.text = currentDialogue.lines[line].speakerName;
        dialogueField.text = currentDialogue.lines[line].dialogue;
        lineNumber = line;
    }

    public void EndDialogue()
    {
        print("Closing dialogue box");

        GameObject.FindAnyObjectByType<LineBehavior>().SelfDestruct();

        currentDialogue = null;

        dialogueBox.SetActive(false);

        inDialogue = false;

        if (TimeManager.callNumber < callManager.days[TimeManager.dayNumber].call.Length - 1)
        {
            TimeManager.callNumber++;

            //Temporary auto assigning of initial notch for a call
            if (!FindAnyObjectByType<ForceAssignNotch>().isActive) return;
            int callNumber = 0;
            for (int i = 0; i < TimeManager.dayNumber; i++) callNumber += callManager.days[i].call.Length;
            callNumber += TimeManager.callNumber;
            if (FindAnyObjectByType<ForceAssignNotch>().autoNotches.Length - 1 <= callNumber) drawLine.SelectPoint(FindAnyObjectByType<ForceAssignNotch>().autoNotches[callNumber].transform.GetChild(1).gameObject);
        }
        else
        {
            print("End of day");

            transitionTargets.FindTargetScene();
        }
    }
}
