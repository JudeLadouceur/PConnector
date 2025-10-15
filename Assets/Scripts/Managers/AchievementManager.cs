using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public Dictionary<AchievementNames, Achievement> achievementDictionary;
    public GameObject viewRoot;
    public Canvas puzzleCanvas;
    public Camera sceneCamera;

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
}
