using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAchieveTrigger : MonoBehaviour
{
    public bool isCharacterEnding;
    public AchievementNames ending;

    public void Start()
    {
        if (isCharacterEnding)
        {
            if(AchievementManager.instance.achievementDictionary.TryGetValue(ending, out Achievement achievement) && achievement.status!=AchievementStatus.Revealed && achievement.status!= AchievementStatus.Placed)
            {
                achievement.Achieve();
            }
        }
        if (AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.TheEnd, out Achievement achievement2) && achievement2.status != AchievementStatus.Revealed && achievement2.status != AchievementStatus.Placed)
        {
            achievement2.Achieve();
        }
    }
}
