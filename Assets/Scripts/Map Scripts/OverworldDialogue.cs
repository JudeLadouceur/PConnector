using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OverworldDialogue : Interactables
{
    [Serializable]
    public class dialogueOption
    {
        [Serializable]
        public class variableInfo
        {
            public DialogueVar variable;
            public int value;
        }
        
        public bool onlyPlaysOnce = false;
        public variableInfo[] otherConditions;
        public SO_Dialogue dialogue;

        [HideInInspector]
        public bool hasBeenPlayed = false;
    }

    public SO_Dialogue dialogue;

    public dialogueOption[] dialogueOptions;
    public Characters character;


    public override void Interact()
    {
        //if (AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.LittleTalks, out Achievement value) && value.status == AchievementStatus.Revealed) value.Achieve();
        
        //Add notes to the notebook
        if (canAddNotes && !NotebookManager.Instance.CheckCharacterTalked(character))
        {
            //Debug.Log("Talking");
            NotebookManager.Instance.CharacterTalked(character);
        }
        
        //Cut the player's movement
        MovementScript.instance.Funny = true;
        MovementScript.instance.canToggle = false;

        //Figure out what dialogue option should be played
        dialogueOption.variableInfo variableInfo = null;
        SO_Dialogue selectedDialogue = null;

        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            Debug.Log("Checking option: " + i);
            Debug.Log("Number of variableInfo checks: " + dialogueOptions[i].otherConditions.Length);

            Debug.Log("Repeatable dialogue: " + !dialogueOptions[i].onlyPlaysOnce + ". Dialogue has been played already: " + dialogueOptions[i].hasBeenPlayed);
            Debug.Log("Should the dialogue play: " + !(dialogueOptions[i].onlyPlaysOnce && dialogueOptions[i].hasBeenPlayed));
            //Check if the dialogue is only supposed to play once and has already been played
            if (!(dialogueOptions[i].onlyPlaysOnce && dialogueOptions[i].hasBeenPlayed))
            {
                //If there are no conditions on the dialogue, just play it
                if (dialogueOptions[i].otherConditions.Length == 0)
                {
                    selectedDialogue = dialogueOptions[i].dialogue;
                    dialogueOptions[i].hasBeenPlayed = true;

                    Debug.Log("No variableInfo checks, playing: " + dialogueOptions[i].dialogue);

                    if (i + 1 < dialogueOptions.Length && !dialogueOptions[i].onlyPlaysOnce) Debug.LogWarning("Unreachable dialogue detected. Dialogue with no requirements is placed above other dialogue possibilities, making them unreachable.");
                    break;
                }

                //Check if all other conditions are met
                for (int o = 0; o < dialogueOptions[i].otherConditions.Length; o++)
                {
                    variableInfo = dialogueOptions[i].otherConditions[o];
                    Debug.Log("Checking: " + variableInfo.variable);

                    //If the variable is incorrect, go to the next dialogue option
                    if (VariableManager.instance.flags[variableInfo.variable] != variableInfo.value)
                    {
                        Debug.Log("variableInfo " + variableInfo.variable + " was " + VariableManager.instance.flags[variableInfo.variable] + ", but was looking for " + variableInfo.value);
                        
                        break;
                    }
                    //If the variable is correct, keep going
                    else
                    {
                        Debug.Log("variableInfo " + variableInfo.variable + " is the correct value.");

                        if (o == dialogueOptions[i].otherConditions.Length - 1)
                        {
                            selectedDialogue = dialogueOptions[i].dialogue;
                            dialogueOptions[i].hasBeenPlayed = true;
                        }
                    }
                }
            }

            //Stop the loop if a valid dialogue has been found
            if (selectedDialogue != null) break;
        }

        //Determine if bark audio should be played
        if (selectedDialogue.isBark)
        {
            if (!selectedDialogue.hasNoAudio)
            {
                if (!selectedDialogue.barkEvent.IsNull)
                {
                    DialogueVoiceManager.Instance.PlayBark(selectedDialogue.barkEvent);
                }
                else Debug.LogWarning(selectedDialogue.name + " dialogue Scriptable Object does not have any audio assigned to it.");
            }
        }

        DialogueManager.Instance.StartDialogue(selectedDialogue, false);
    }
}
