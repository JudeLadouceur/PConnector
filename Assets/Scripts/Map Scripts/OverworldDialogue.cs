using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldDialogue : MonoBehaviour
{
    public SO_Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogueManager.Instance.StartDialogue(dialogue);

            if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.LittleTalks, out Achievement value) && value.status == AchievementStatus.Revealed)
            {
                value.Achieve();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DialogueManager.Instance.EndDialogue();
    }
}
