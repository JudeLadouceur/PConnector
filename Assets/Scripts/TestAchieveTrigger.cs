using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAchieveTrigger : MonoBehaviour
{
    public AchievementNames targetName;

    public void HideAchievement()
    {
        Achievement target;
        AchievementManager.instance.achievementDictionary.TryGetValue(targetName, out target);
        target.SetStatus(AchievementStatus.Hidden);
    }

    public void RevealAchievement()
    {
        Achievement target;
        AchievementManager.instance.achievementDictionary.TryGetValue(targetName, out target);
        target.SetStatus(AchievementStatus.Revealed);
    }

    public void AchievedAchievement()
    {
        Achievement target;
        AchievementManager.instance.achievementDictionary.TryGetValue(targetName, out target);
        target.SetStatus(AchievementStatus.Achieved);
    }
}
