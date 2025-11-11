using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private GameObject dialogueBox;
    private CallManager callManager;
    private DrawLine drawLine;
    private CharacterManager characterManager;
    private SceneTransitionTargets transitionTargets;


    public int lineNumber;
    private bool inDialogue = false;

    private SO_Dialogue currentDialogue;
    private TextMeshProUGUI speakerField;
    private TextMeshProUGUI dialogueField;

    private int currentChunkIndex = 0;

    private EventInstance audioSource;

    void Start()
    {
        Instance = this;

        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        characterManager = GameObject.FindAnyObjectByType<CharacterManager>();

        speakerField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 2).GetComponent<TextMeshProUGUI>();
        dialogueField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 1).GetComponent<TextMeshProUGUI>();
        dialogueBox.SetActive(false);

        SceneManager.activeSceneChanged += SceneTransition;
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

            if (audioSource.isValid())
            {
                audioSource.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Immediately end the dialogue.
            }
           
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

        if (!currentDialogue.voiceLineEvent.IsNull)
        {
            audioSource = DialogueVoiceManager.Instance.PlayVoiceLine(currentDialogue.voiceLineEvent, lineNumber);
        }
        else
        {
            Debug.LogWarning("A dialogue Scriptable Object does not have an audio Event assigned to it.");
        }
    }

    public void EndDialogue()
    {
        print("Closing dialogue box");

        currentDialogue = null;

        dialogueBox.SetActive(false);

        inDialogue = false;

        if (SceneManager.GetActiveScene().name != "Switchboard") return;

        GameObject.FindAnyObjectByType<LineBehavior>().SelfDestruct();

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

            string target = "Day "+ (TimeManager.dayNumber+1) +" - Afterwork ";

            transitionTargets.FindTargetScene(target);
        }
    }

    private void SceneTransition(UnityEngine.SceneManagement.Scene scene1, UnityEngine.SceneManagement.Scene scene2)
    {
        string sceneName = scene2.name;
        if (sceneName == "Switchboard") FindSwitchboardReferences();
    }

    private void FindSwitchboardReferences()
    {
        callManager = GameObject.FindAnyObjectByType<CallManager>();
        drawLine = GameObject.FindAnyObjectByType<DrawLine>();
        
        transitionTargets = GameObject.FindAnyObjectByType<SceneTransitionTargets>();
    }
    
}
