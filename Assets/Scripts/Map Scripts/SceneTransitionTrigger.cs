using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionTrigger : Interactables
{
    private bool interacted = false;

    public bool goToSwitchboard;

    public override void Interact()
    {
        NextScene();
    }

    public void NextScene()
    {
        if (interacted) return;
        interacted = true;
        if(!goToSwitchboard) SceneManager.instance.GoToNextScene();
        else if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadSceneWithFade("Switchboard");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Switchboard");
        }
    }

    public void NextDay()
    {
        TimeManager.dayNumber++;

        Debug.Log("Day number: " + TimeManager.dayNumber);

        TimeManager.callNumber = 0;
        NextScene();
    }
}
