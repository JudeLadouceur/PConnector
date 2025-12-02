using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTargets : MonoBehaviour
{
    [Serializable]
    public class Days
    {
        public DialogueVar[] dayVariables;
    }

    public Days[] days;

    public void FindTargetScene(string targetName)
    {
        string targetEnd = "";
        List<string> Vars = new List<string>();

        for (int i = 0; i < days[TimeManager.dayNumber].dayVariables.Length; i++)
        {
            if (VariableManager.instance.flags.TryGetValue(days[TimeManager.dayNumber].dayVariables[i], out int var))
            {
                if (var == 1)
                    Vars.Add("A");
                else if (var == 0)
                    Vars.Add("B");
                else
                    Debug.Log("Oh god oh no what has happened a bool is now 2");
            }
        }

        /*if(AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.NewJobBlues, out Achievement achieve) &&achieve.status!=AchievementStatus.Placed)
        {
            Debug.Log("Reveal NewJobBlues");
            achieve.Achieve();
        }*/
        
        for (int i = 0; i < Vars.Count; i++)
        {
            if (i != 0) targetEnd += " - ";
            targetEnd += Vars[i];
        }

        targetName = targetName + targetEnd;

        SceneManager.LoadScene(targetName);

        //Vars.Clear();
    }
}
