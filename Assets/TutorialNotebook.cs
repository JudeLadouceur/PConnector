using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialNotebook : MonoBehaviour
{
    public static TutorialNotebook instance;
    //Fill out notes in book
    public GameObject infoRoot;
    public GameObject bulletTextPrefab;
    public CPersonNote[] notes;
    //Current maximum based on system
    private int maxNotes = 11;

    //toggle button stuff
    public GameObject toggleButton;
    public Image toggleIcon;
    public Sprite closedIcon;
    public Sprite openIcon;

    //objects for open and close
    public GameObject bookObjects;
    public GameObject openTarget;
    public GameObject closeTarget;

    //Active and open tracking
    private bool isOpen;
    private bool canBeActive = false;

    //Messy skip conversation variables for unlocking hints
    private Dictionary<Characters, bool> characterTalked;

    private List<GameObject> bulletPoints;

    private void Start()
    {
        instance=this;
        //Check for too many notes
            if(notes.Length > maxNotes)
            {
                Debug.LogError("One or more days has more than " + maxNotes + " notes assigned to it. Please make sure you have a maximum of " + maxNotes + " notes assigned to any given day. Notes may look odd or go off screen.");
            }

        //Dictionary of characters
        characterTalked = new Dictionary<Characters, bool>();
        Characters[] characters = (Characters[])System.Enum.GetValues(typeof(Characters));
        foreach(Characters c in characters)
        {
            if (c != Characters.None)
            {
                characterTalked.Add(c, false);
            }
        }
        bulletPoints = new List<GameObject>();
        UpdateNotebook();
    }

    public void NotebookCheckScene(Scene scene1, Scene scene2)
    {
        //Toggle notebook closed at new scene
        if (isOpen)
        {
            ToggleNotebook();
        }
        EnableDisableNotebook();

        //Check if need to update infor for day
        
    }
    public void ToggleNotebook()
    {
        if (isOpen)
        {
            toggleIcon.sprite = closedIcon;
            bookObjects.transform.position = closeTarget.transform.position;
            isOpen = false;
            if(FMODSoundPlayer.Instance!=null)
                FMODSoundPlayer.Instance.PlayFMODSound(1);
        } else
        {
            toggleIcon.sprite = openIcon;
            bookObjects.transform.position=openTarget.transform.position;
            isOpen = true;

            if(FMODSoundPlayer.Instance != null)
                FMODSoundPlayer.Instance.PlayFMODSound(0);

        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void UpdateNotebook()
    {
        
        foreach (GameObject i in bulletPoints)
        {
            Destroy(i);
        }
        foreach(CPersonNote note in notes)
        {
            if(characterTalked.TryGetValue(note.character, out bool talked))
            {
                if (talked)
                {
                    AddNote(note);
                }
            } else if (note.character==Characters.None)
            {
                AddNote(note);
            }
        }
    }

    private void EnableDisableNotebook()
    {
        if (canBeActive)
        {
            toggleButton.gameObject.SetActive(true);

        } else
        {
            toggleButton.gameObject.SetActive(false);
        }
    }

    public void CharacterTalked(Characters person)
    {
        characterTalked[person] = true;
        UpdateNotebook();
    }

    public bool CheckCharacterTalked(Characters person)
    {
        return characterTalked[person];
    }

    private void AddNote(CPersonNote n)
    {
        if (n.usesVariable)
        {
            if(VariableManager.instance.flags.TryGetValue(n.varName, out int val))
            {
                if (val != 0)
                {
                    GameObject temp = Instantiate(bulletTextPrefab, infoRoot.transform);
                    bulletPoints.Add(temp);
                    temp.GetComponent<NotebookTextUpdate>().SetText(n.note);
                }
                else
                {
                    GameObject temp = Instantiate(bulletTextPrefab, infoRoot.transform);
                    bulletPoints.Add(temp);
                    temp.GetComponent<NotebookTextUpdate>().SetText(n.varFalseNote);
                }
            }
            else
            {
                Debug.Log("No variable found for note, check your spelling");
            }
            
        } else
        {
            
            GameObject temp = Instantiate(bulletTextPrefab, infoRoot.transform);
            bulletPoints.Add(temp);
            temp.GetComponent<NotebookTextUpdate>().SetText(n.note);
        }
            
    }
}
