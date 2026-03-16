using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDialogue : OverworldDialogue
{
    public DialogueVar frogVariable;
    private bool talkedPrior;

    public override void Interact()
    {
        base.Interact();
        if (talkedPrior) return;
        if (VariableManager.instance.flags[DialogueVar.FrogTalkedDaysCount] == TimeManager.dayNumber)
        {
            VariableManager.instance.flags[DialogueVar.FrogTalkedDaysCount]++;
            Debug.Log("Talked to frog and triggered variable");
            Debug.Log(VariableManager.instance.flags[DialogueVar.FrogTalkedDaysCount]);
        }
        talkedPrior = true;
    }
}
