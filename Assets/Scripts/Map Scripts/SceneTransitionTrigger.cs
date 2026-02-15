using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionTrigger : Interactables
{
    private bool interacted = false;

    public override void Interact()
    {
        if (interacted) return;
        interacted = true;
        NextScene();
    }

    private void NextScene()
    {
        SceneManager.instance.GoToNextScene();

    }

    public void NextDay()
    {
        if (interacted) return;
        interacted = true;

        TimeManager.dayNumber++;

        Debug.Log("Day number: " + TimeManager.dayNumber);

        TimeManager.callNumber = 0;
        NextScene();
    }
}
