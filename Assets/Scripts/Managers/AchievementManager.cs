using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
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

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        achievementDictionary = new Dictionary<AchievementNames, Achievement>();
        SceneManager.activeSceneChanged += SetNewCameraInScene;
        SetNewCameraInScene(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
    }

    public void ToggleAchievementView()
    {
        viewRoot.SetActive(!viewRoot.activeInHierarchy);
        if (viewRoot.activeInHierarchy)
        {
            MovementScript player = FindObjectOfType<MovementScript>();
            if (player)
            {
                viewRoot.transform.position = player.gameObject.transform.position;
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
        if(achievementDictionary.TryGetValue(AchievementNames.NewJobBlues, out Achievement value))
        {
            if(value.status == AchievementStatus.Hidden)
            {
                if (CheckNumberAchieved() >= 3)
                {
                    value.Reveal();
                    endDayRevealed = true;
                }
            }
        }
    }
}
