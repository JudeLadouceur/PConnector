using FMOD.Studio;
using FMODUnity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class DaySixDialogueManager : MonoBehaviour
{
    private GameObject dialogueBox;
    private DaySixCallManager callManager;

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

        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");

        speakerField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 2).GetComponent<TextMeshProUGUI>();
        dialogueField = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 1).GetComponent<TextMeshProUGUI>();
        speakerProfile = dialogueBox.transform.GetChild(dialogueBox.transform.childCount - 3).GetComponent<UnityEngine.UI.Image>();
        callManager = FindObjectOfType<DaySixCallManager>();
        speakerProfile.sprite = speakerProfilePictures[0];
        dialogueBox.SetActive(false);
    }

    public void Update()
    {
        audioSource.getPlaybackState(out PLAYBACK_STATE state);
        if (inDialogue && state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            NextLine();
        }
    }

    public void StartDialogue(SO_Dialogue dialogue)
    {
        inDialogue = true;

        dialogueBox.SetActive(true);


        currentDialogue = dialogue;
        

        lineNumber = 0;
        SetDialogueLine(0);

        

    
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
        else
        {
            Debug.LogWarning("A dialogue Scriptable Object does not have a VOICE LINE Event assigned to it.");
        }
    }

    public void EndDialogue()
    {
        currentDialogue = null;

        dialogueBox.SetActive(false);

        inDialogue = false;

        if (audioSource.isValid())
        {
            audioSource.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Immediately end the dialogue.
        }

        callManager.EndCall();
    }
}
