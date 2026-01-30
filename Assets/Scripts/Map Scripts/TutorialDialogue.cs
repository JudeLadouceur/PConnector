using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class TutorialDialogue : Interactables
{
    public SO_Dialogue dialogue;
    public Characters character;
    public int stepIndex;

    public override void Interact()
    {
        //if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.LittleTalks, out Achievement value) && value.status == AchievementStatus.Revealed) value.Achieve();
        TestTargetSwap.instance.AttemptProgress(stepIndex);
        if (canAddNotes && !TutorialNotebook.instance.CheckCharacterTalked(character))
        {
            //Debug.Log("Talking");
            TutorialNotebook.instance.CharacterTalked(character);
        }
        
        MovementScript.instance.Funny = true;
        MovementScript.instance.canToggle = false;

        
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
}
