using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class OverworldDialogue : Interactables
{
    public SO_Dialogue dialogue;
    public Characters character;

    public override void Interact()
    {
        //if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.LittleTalks, out Achievement value) && value.status == AchievementStatus.Revealed) value.Achieve();
        
        if (canAddNotes && !NotebookManager.Instance.CheckCharacterTalked(character))
        {
            //Debug.Log("Talking");
            NotebookManager.Instance.CharacterTalked(character);
        }
        
        MovementScript.instance.Funny = true;
        MovementScript.instance.canToggle = false;

        if (dialogue.isBark)
        {
            if (!dialogue.hasNoAudio)
            {
                if (!dialogue.barkEvent.IsNull)
                {
                    DialogueVoiceManager.Instance.PlayBark(dialogue.barkEvent);
                }
                else Debug.LogWarning(dialogue.name + " dialogue Scriptable Object does not have any audio assigned to it.");
            }
        }

        DialogueManager.Instance.StartDialogue(dialogue, false);
    }
}
