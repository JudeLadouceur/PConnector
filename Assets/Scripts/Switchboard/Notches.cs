using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notches : MonoBehaviour
{
    [HideInInspector]
    public bool isOccupied = false;

    public Characters assignedCharacter;
    

    private void OnMouseDown()
    {
        if (!LineManager.instance.canDraw) return;
        if (isOccupied) return;
        if (assignedCharacter == Characters.None) return;
        if (DialogueManager.Instance.inDialogue) return;
        LineManager.instance.SelectPoint(gameObject);
        isOccupied = true;
        if (TutorialSwitchboard.instance != null)
            TutorialSwitchboard.instance.NoPrompt();
    }
}
