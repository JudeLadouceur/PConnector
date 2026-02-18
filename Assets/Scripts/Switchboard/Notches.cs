using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notches : MonoBehaviour
{
    //[HideInInspector]
    public bool isOccupied = false;

    public Characters assignedCharacter;
    

    private void OnMouseDown()
    {
        Debug.Log("check 1");
        if (!LineManager.instance.canDraw) return;
        Debug.Log("check 2");
        if (isOccupied) return;
        Debug.Log("check 3");
        if (assignedCharacter == Characters.None) return;
        Debug.Log("check 4");
        if (DialogueManager.Instance.inDialogue) return;
        Debug.Log("All good");
        LineManager.instance.SelectPoint(gameObject);
        isOccupied = true;
        if (TutorialSwitchboard.instance != null)
            TutorialSwitchboard.instance.NoPrompt();
    }
}
