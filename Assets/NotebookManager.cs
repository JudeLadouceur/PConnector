using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NotebookManager : MonoBehaviour
{
    public static NotebookManager Instance;

    //Fill out notes in book
    public GameObject infoRoot;
    public GameObject bulletTextPrefab;
    public CNotes[] days;
    //Current maximum based on system
    private int maxNotes = 11;

    //Scenes the system should be active in
    public string[] approvedSceneFragments;

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
    private int currentDay = -100;

    //Messy skip conversation variables for unlocking hints
    public bool conversationSkip = false;
    private Dictionary<Characters, bool> characterTalked;

    private List<GameObject> bulletPoints;

    private string[] dayNames;
    private bool showNotoif = false;
    public GameObject notifObject;


    private void Start()
    {
        //Manager instance
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(this.gameObject);
            return;
        }

        dayNames = new string[5] {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday"};
        //Check for too many notes
        foreach(CNotes noteSet in days)
        {
            if(noteSet.notes.Length > maxNotes)
            {
                Debug.LogError("One or more days has more than " + maxNotes + " notes assigned to it. Please make sure you have a maximum of " + maxNotes + " notes assigned to any given day. Notes may look odd or go off screen.");
            }
        }

        //Dictionary of characters
        characterTalked = new Dictionary<Characters, bool>();
        Characters[] characters = (Characters[])System.Enum.GetValues(typeof(Characters));
        foreach(Characters c in characters)
        {
            if (c != Characters.None)
            {
                characterTalked.Add(c, conversationSkip);
            }
        }
        bulletPoints = new List<GameObject>();

        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += NotebookCheckScene;
        NotebookCheckScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene(), UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }

    public void NotebookCheckScene(Scene scene1, Scene scene2)
    {
        //Toggle notebook closed at new scene
        if (isOpen)
        {
            ToggleNotebook();
        }

        //check if notebook allowed in current scene
        canBeActive = false;
        foreach(string frag in approvedSceneFragments)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains(frag))
            {
                canBeActive = true;
            }
        }
        EnableDisableNotebook();

        //Check if need to update infor for day
        if(TimeManager.dayNumber != currentDay)
        {
            currentDay = TimeManager.dayNumber;
            Characters[] characters = (Characters[])System.Enum.GetValues(typeof(Characters));
            foreach (Characters c in characters)
            {
                if (c != Characters.None)
                {
                    characterTalked[c] = conversationSkip;
                }
            }
            UpdateNotebook();
        }
    }
    public void ToggleNotebook()
    {
        if (!canBeActive) return;
        if (isOpen)
        {
            toggleIcon.sprite = closedIcon;
            bookObjects.transform.position = closeTarget.transform.position;
            isOpen = false;

            FMODSoundPlayer.Instance.PlayFMODSound(1);
        } else
        {
            toggleIcon.sprite = openIcon;
            bookObjects.transform.position=openTarget.transform.position;
            isOpen = true;
            FMODSoundPlayer.Instance.PlayFMODSound(0);
            //NotebookHeader.instance.UpdateHeader();
            EndNotif();
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void UpdateNotebook()
    {
        foreach(GameObject i in bulletPoints)
        {
            Destroy(i);
        }
        if(currentDay > days.Length)
        {
            Debug.LogError("Your current day exceeds the notebook days array. Please ensure the array has all days in the current build.");
        }
        foreach(CPersonNote note in days[currentDay].notes)
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

        //Debug.Log(dayNames[currentDay]);
        string headerText = dayNames[currentDay] + " - Day " + (currentDay + 1).ToString() + "/5";
        NotebookHeader.instance.UpdateHeader(headerText);
        StartNotif();
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
                    if (n.note == "") return;
                    GameObject temp = Instantiate(bulletTextPrefab, infoRoot.transform);
                    bulletPoints.Add(temp);
                    temp.GetComponent<NotebookTextUpdate>().SetText(n.note);
                }
                else
                {
                    if (n.varFalseNote == "") return;
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
            if (n.note == "") return;
            GameObject temp = Instantiate(bulletTextPrefab, infoRoot.transform);
            bulletPoints.Add(temp);
            temp.GetComponent<NotebookTextUpdate>().SetText(n.note);
        }
            
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleNotebook();
        }
    }

    private void StartNotif()
    {
        notifObject.SetActive(true);
        showNotoif = true;
    }

    private void EndNotif()
    {
        notifObject.SetActive(false);
        showNotoif = false;
    }
}
