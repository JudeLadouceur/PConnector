using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class OverworldDialogue : MonoBehaviour
{
    public SO_Dialogue dialogue;

    private bool isInteractable;

    private GameObject interactPrompt;
    private GameObject promptRef;

    private void Start()
    {
        interactPrompt = transform.parent.GetChild(1).gameObject;
        interactPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteractable = true;
            interactPrompt.SetActive(true);
        }
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

        MovementScript.instance.Funny = true;

        DialogueManager.Instance.StartDialogue(dialogue);

        // If the Event is not null and assigned in the Inspector, play the bark line (at random based on the FMOD setup).
        if (!dialogue.barkEvent.IsNull)
        {
            BarkManager.Instance.PlayBark(dialogue.barkEvent);
        }
        else
        {
            Debug.LogWarning("A dialogue Scriptable Object does not have a BARK Event assigned to it.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) isInteractable = false;
        interactPrompt.SetActive(false);
    }
}
