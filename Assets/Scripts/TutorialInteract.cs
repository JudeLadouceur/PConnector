using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteract : Interactables
{
    public Transform moveableObject;
    public Vector3 moveToRelative;
    public int stepIndex;
    public Animator anim;
    public SO_Dialogue dialogue;

    public override void Interact()
    {
        base.Interact();
        Vector3 moveTarget = moveableObject.position + moveToRelative;
        moveableObject.position = moveTarget;
        anim.SetBool("Triggered", true);
        if(TestTargetSwap.instance!=null && TestTargetSwap.instance.currentStep == stepIndex)
        {
            TestTargetSwap.instance.AttemptProgress(stepIndex);
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
