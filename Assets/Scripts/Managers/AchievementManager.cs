using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public Dictionary<AchievementNames, Achievement> achievementDictionary;
    public GameObject viewRoot;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        achievementDictionary = new Dictionary<AchievementNames, Achievement>();
    }

    public void ToggleAchievementView()
    {
        viewRoot.SetActive(!viewRoot.activeInHierarchy);
    }
}
