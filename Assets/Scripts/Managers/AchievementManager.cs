using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public Dictionary<AchievementNames, Achievement> achievementDictionary;


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
            Debug.Log("Two Achievement Managers Attempting To Exist, Destroying New New Copy");
        }
        Achievement[] achievementPieces = GameObject.FindObjectsOfType<Achievement>();
        achievementDictionary = new Dictionary<AchievementNames, Achievement>();
        foreach(Achievement achieve in achievementPieces)
        {
            if (!achievementDictionary.TryAdd(achieve.achievementName, achieve))
            {
                Debug.LogError("ERROR: Attempted to add an achievement with a name already in the achievement dictionary. Check and ensure all achivements in the scene have a unique name enumerator.");
            }
        }
    }
}
