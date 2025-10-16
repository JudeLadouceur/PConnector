using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTargets : MonoBehaviour
{
    string targetName = "Day 1 - Afterwork ";

    public void FindTargetScene()
    {
        string targetEnd = "";
        string firstVar="";
        string secondVar="";
        if(CharacterManager.instance.flags.TryGetValue("walkerFixed", out int var))
        {
            if (var == 1)
                firstVar = "A";
            else if (var == 0)
                firstVar = "B";
            else
                Debug.Log("Oh god oh no what has happened a bool is now 2");
        }
        if (CharacterManager.instance.flags.TryGetValue("partsGained", out int var2))
        {
            if (var2 == 1)
                secondVar = "A";
            else if (var2 == 0)
                secondVar = "B";
            else
                Debug.Log("Oh god oh no what has happened a bool is now 2");
        }
        if(AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.NewJobBlues, out Achievement achieve) &&achieve.status!=AchievementStatus.Placed)
        {
            Debug.Log("Reveal NewJobBlues");
            achieve.Achieve();
        }
        targetEnd = firstVar + " - " + secondVar;
        targetName = targetName + targetEnd;

        SceneManager.LoadScene(targetName);
    }
}
