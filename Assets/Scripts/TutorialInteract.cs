using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteract : Interactables
{
    public Transform moveableObject;
    public Vector3 moveToRelative;
    public int stepIndex;
    public Animator anim;


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
    }
}
