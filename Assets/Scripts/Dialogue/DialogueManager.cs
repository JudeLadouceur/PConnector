using FMOD.Studio;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private GameObject dialogueBox;
    private CallManager callManager;
    private VariableManager characterManager;
    private SceneTransitionTargets transitionTargets;
    private bool doNotProgessToNextCall;

    public int lineNumber;
    public bool inDialogue = false;

    public Sprite[] speakerProfilePictures;

    private SO_Dialogue currentDialogue;
    private TextMeshProUGUI speakerField;
    private TextMeshProUGUI dialogueField;
    private UnityEngine.UI.Image speakerProfile;

    private EventInstance audioSource;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        characterManager = GameObject.FindAnyObjectByType<VariableManager>();

        speakerField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 2).GetComponent<TextMeshProUGUI>();
        dialogueField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 1).GetComponent<TextMeshProUGUI>();
        speakerProfile = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 3).GetComponent<UnityEngine.UI.Image>();
        speakerProfile.sprite = speakerProfilePictures[0];
        dialogueBox.SetActive(false);

        SceneManager.activeSceneChanged += SceneTransition;

        if(SceneManager.GetActiveScene().name.Contains("witchboard")) FindSwitchboardReferences();
    }

    private void Update()
    {
        if (inDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space)) NextLine();
        }
    }

    public void StartDialogue(SO_Dialogue dialogue, bool doNotProgress)
    {
        print("Begin dialogue " + dialogue.name);

        dialogueBox.SetActive(true);

        doNotProgessToNextCall = doNotProgress;

        currentDialogue = dialogue;
        

        lineNumber = 0;
        SetDialogueLine(0);

        for (int i = 0; i < dialogue.variables.Length; i++) 
        {
            characterManager.flags[dialogue.variables[i].variable] = dialogue.variables[i].value;
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
            EndDialogue();
            return;
        }

        print("Go to line " + line);

        string speakerName = currentDialogue.lines[line].NewSpeakerName.ToString();

        if (speakerName == "Perkins") speakerName = "Mrs. Perkins";

        speakerField.text = speakerName;
        dialogueField.text = currentDialogue.lines[line].dialogue;

        if (speakerProfile.sprite == null || speakerProfile.sprite.name != speakerName)
        {
            speakerProfile.sprite = null;

            for (int i = 0; i < speakerProfilePictures.Length; i++)
            {
                if (speakerName == speakerProfilePictures[i].name)
                {
                    speakerProfile.sprite = speakerProfilePictures[i];
                    break;
                }
            }

            if (speakerProfile.sprite == null) Debug.LogWarning("There is no profile picture with the same name as the character");
        }

        lineNumber = line;

        if (!currentDialogue.voiceLineEvent.IsNull)
        {
            audioSource = DialogueVoiceManager.Instance.PlayVoiceLine(currentDialogue.voiceLineEvent, lineNumber);
        }
        else if (!currentDialogue.isBark)
        {
            Debug.LogWarning(currentDialogue.name + " dialogue Scriptable Object does not have any audio assigned to it.");
        }
    }

    public void EndDialogue()
    {
        print("Closing dialogue box");

        currentDialogue = null;

        dialogueBox.SetActive(false);

        inDialogue = false;

        if (audioSource.isValid())
        {
            audioSource.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Immediately end the dialogue.
        }

        if (!SceneManager.GetActiveScene().name.Contains("witchboard"))
        {
            MovementScript.instance.Funny = false;
            MovementScript.instance.canToggle = true;
            return;
        }

        if (callManager.inContextCall)
        {
            callManager.inContextCall = false;

            ActivateNotchSelect();

            return;
        }

        GameObject.FindAnyObjectByType<LineBehavior>().SelfDestruct();

        if (doNotProgessToNextCall)
        {
            ActivateNotchSelect();
        }

        else if (TimeManager.callNumber < callManager.days[TimeManager.dayNumber].call.Length - 1)
        {
            TimeManager.callNumber++;

            callManager.StartCoroutine(callManager.StartCallDelay());
        }
        else EndDay();
    }

    private void SceneTransition(UnityEngine.SceneManagement.Scene scene1, UnityEngine.SceneManagement.Scene scene2)
    {
        if (scene2.name.Contains("witchboard")) FindSwitchboardReferences();
        if (scene2.name == "Main Menu") ResetDialogue();
        if (inDialogue)
        {
            currentDialogue = null;

            dialogueBox.SetActive(false);

            inDialogue = false;

            if (audioSource.isValid())
            {
                audioSource.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Immediately end the dialogue.
            }

        }
    }

    private void FindSwitchboardReferences()
    {
        Debug.Log("Switchboarding...");
        callManager = GameObject.FindAnyObjectByType<CallManager>();
        
        transitionTargets = GameObject.FindAnyObjectByType<SceneTransitionTargets>();
    }
    
    public void EndDay()
    {
        print("End of day");
        if (TutorialSwitchboard.instance != null)
        {
            SceneManager.LoadScene("Tutorial World");
        } else
        {
            string target = "Day " + (TimeManager.dayNumber + 1) + " - Afterwork ";

            transitionTargets.FindTargetScene(target);
        }


            
    }

    private void ResetDialogue()
    {
        TimeManager.dayNumber = 0;
        TimeManager.callNumber = 0;
        VariableManager.instance.ResetFlags();

        Debug.Log("Time manager reset. Day number now: " + TimeManager.dayNumber);
    }

    private void ActivateNotchSelect()
    {
        Notches[] notches = GameObject.FindGameObjectWithTag("NotchParent").GetComponentsInChildren<Notches>();
        if (TutorialSwitchboard.instance != null)
            TutorialSwitchboard.instance.EndContextCall();

        GameObject target;

        LineManager.instance.canDraw = true;

        foreach (Notches notch in notches)
        {
            if (notch.assignedCharacter == callManager.days[TimeManager.dayNumber].call[TimeManager.callNumber].caller)
            {
                target = notch.gameObject;
                notch.isOccupied = true;
                LineManager.instance.SelectPoint(target);
                break;
            }
        }
    }
}
