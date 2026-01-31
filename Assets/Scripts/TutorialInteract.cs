using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteract : Interactables
{
    public Transform moveableObject;
    public Vector3 moveToRelative;
    public int stepIndex;


    public override void Interact()
    {
        base.Interact();
        Vector3 moveTarget = moveableObject.position + moveToRelative;
        moveableObject.position = moveTarget;
        if(TestTargetSwap.instance!=null && TestTargetSwap.instance.currentStep == stepIndex)
        {
            TestTargetSwap.instance.AttemptProgress(stepIndex);
        }
    }
}
