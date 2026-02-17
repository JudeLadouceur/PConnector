using UnityEngine;

public class TutorialDialogue : Interactables
{
    public SO_Dialogue dialogue;
    public Characters character;
    public int stepIndex;

    public override void Interact()
    {
        //if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.LittleTalks, out Achievement value) && value.status == AchievementStatus.Revealed) value.Achieve();
        
        if (canAddNotes && !TutorialNotebook.instance.CheckCharacterTalked(character))
        {
            //Debug.Log("Talking");
            TutorialNotebook.instance.CharacterTalked(character);
        }
        if(character == Characters.Perkins)
        {
            if (TestTargetSwap.instance.currentStep == stepIndex)
            {
                
                if (AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.TheEnd, out Achievement keyAchieve))
                {
                    keyAchieve.Achieve();
                }
            }
            
        }
        TestTargetSwap.instance.AttemptProgress(stepIndex);
        MovementScript.instance.Funny = true;
        MovementScript.instance.canToggle = false;

        if (dialogue.isBark)
        {
            if (!dialogue.barkEvent.IsNull)
            {
                DialogueVoiceManager.Instance.PlayBark(dialogue.barkEvent);
            }
            else Debug.LogWarning(dialogue.name + " dialogue Scriptable Object does not have any audio assigned to it.");
        }
        

        DialogueManager.Instance.StartDialogue(dialogue, false);


    }
}
