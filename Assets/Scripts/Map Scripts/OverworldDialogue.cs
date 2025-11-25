using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldDialogue : MonoBehaviour
{
    public SO_Dialogue dialogue;

    private bool isInteractable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) isInteractable = true;
    }

    private void Update()
    {
        if (!isInteractable) return;

        if (Input.GetButtonDown("Interact")) Interact();
    }

    private void Interact()
    {
        if (!isInteractable) return;

        if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.LittleTalks, out Achievement value) && value.status == AchievementStatus.Revealed) value.Achieve();

        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) isInteractable = false;
    }
}
