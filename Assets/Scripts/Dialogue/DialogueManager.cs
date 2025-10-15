using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private GameObject dialogueBox;
    private int lineNumber;
    private bool inDialogue = false;

    private SO_Dialogue currentDialogue;
    private TextMeshProUGUI speakerField;
    private TextMeshProUGUI dialogueField;

    void Start()
    {
        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        speakerField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 2).GetComponent<TextMeshProUGUI>();
        dialogueField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 1).GetComponent<TextMeshProUGUI>();
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (inDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space)) return;
        }
    }

    public void StartDialogue(SO_Dialogue dialogue)
    {
        print("Begin dialogue " + dialogue.name);

        dialogueBox.SetActive(true);

        currentDialogue = dialogue;
        

        lineNumber = 0;
        SetDialogueLine(0);

        

        inDialogue = true;
    }

    //If this is not the last line, increment the line number by 1 and play the new line
    public void NextLine()
    {
        if (!inDialogue) return;
        if (lineNumber == currentDialogue.lines.Count - 1) return;
        else
        {
            lineNumber++;
            SetDialogueLine(lineNumber);
        }
    }

    //Start the line indicated by the received number
    public void SetDialogueLine(int line)
    {
        if (!inDialogue) return;
        if(line >= currentDialogue.lines.Count)
        {
            Debug.LogError("This line is out of range, this script can only use a line number that is " + (currentDialogue.lines.Count - 1) + " or lower");
        }
        print("Go to line " + line);
        speakerField.text = currentDialogue.lines[line].speakerName;
        dialogueField.text = currentDialogue.lines[line].dialogue;
        lineNumber = line;
    }

    public void EndDialogue()
    {
        print("Closing dialogue");

        currentDialogue = null;

        dialogueBox.SetActive(false);

        inDialogue = false;
    }
}
