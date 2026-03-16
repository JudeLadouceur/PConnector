using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDialogue : OverworldDialogue
{
    public DialogueVar[] frogVariables;
    private bool talkedPrior;

    public override void Interact()
    {
        base.Interact();
        if (talkedPrior || VariableManager.instance.flags[frogVariables[TimeManager.dayNumber]]>0) return;
        if ((TimeManager.dayNumber==0) || (VariableManager.instance.flags[frogVariables[TimeManager.dayNumber - 1]] > 0))
        {
            VariableManager.instance.flags[frogVariables[TimeManager.dayNumber]] = 1;
            Debug.Log("Talked to frog and triggered variable " + frogVariables[TimeManager.dayNumber]);
        }
            talkedPrior = true;
    }
}
