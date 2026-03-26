using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPromptButton : MonoBehaviour
{
    private Interactables interactable;

    public void SetInteractable(Interactables i)
    {
        if (interactable == null) interactable = i;
    }

    public void OnClick()
    {
        interactable.Interact();
    }
}
