using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public Dictionary<AchievementNames, Achievement> achievementDictionary;
    public GameObject viewRoot;
    public Canvas puzzleCanvas;
    public Camera sceneCamera;
    public bool endDayRevealed = false;
    public AchieveNameClass[] namesCorrelation;
    public Dictionary<AchievementNames,string> namesDict;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
        achievementDictionary = new Dictionary<AchievementNames, Achievement>();
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SetNewCameraInScene;
        SetNewCameraInScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene(), UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        
        namesDict = new Dictionary<AchievementNames, string>();
        foreach(AchieveNameClass nameObj in namesCorrelation)
        {
            namesDict.Add(nameObj.name, nameObj.displayName);
        }
        viewRoot.SetActive(true);
        viewRoot.SetActive(false);
    }

    public void ToggleAchievementView()
    {
        viewRoot.SetActive(!viewRoot.activeInHierarchy);
        if (!FindObjectOfType<MovementScript>()) return;
        MovementScript player = FindObjectOfType<MovementScript>();
        if (player)
        {
            player.achieveFunny = viewRoot.activeInHierarchy;
        }
        if (viewRoot.activeInHierarchy)
        {
            if (FindObjectOfType<MovementScript>())
            {
                if (player)
                {
                    viewRoot.transform.position = player.gameObject.transform.position;
                }
            }
            
        }
    }

    void SetNewCameraInScene(Scene scene, Scene scene2)
    {
        if(puzzleCanvas.worldCamera == null)
        {
            puzzleCanvas.worldCamera = Camera.main;
        }
        viewRoot.transform.position = new Vector3(0, 0, 0);
    }
    public int CheckNumberAchieved()
    {
        Achievement[] achievements = achievementDictionary.Values.ToArray();
        int achievedCount = 0;
        foreach(Achievement achievement in achievements)
        {

            if (achievement.status == AchievementStatus.Achieved || achievement.status==AchievementStatus.Placed)
            {
                achievedCount++;
            }
        }
        return achievedCount;
    }
    public void CheckForEndDayAchieveReveal()
    {
        /*if(achievementDictionary.TryGetValue(AchievementNames.NewJobBlues, out Achievement value))
        {
            if(value.status == AchievementStatus.Hidden)
            {
                if (CheckNumberAchieved() >= 3)
                {
                    value.Reveal();
                    endDayRevealed = true;
                }
            }
        }*/
    }
}
