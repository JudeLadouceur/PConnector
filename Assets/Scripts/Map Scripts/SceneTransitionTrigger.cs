using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionTrigger : Interactables
{
    private bool interacted = false;
    public string Scene;

    public override void Interact()
    {
        if (interacted) return;
        interacted = true;
        if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadSceneWithFade(Scene);
        }
        else
        {
            SceneManager.LoadScene(Scene);
        }
    }

    public void NextScene()
    {
        if (interacted) return;
        interacted = true;
        if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadSceneWithFade(Scene);
        }
        else
        {
            SceneManager.LoadScene(Scene);
        }
    }

    public void NextDay()
    {
        if (interacted) return;
        interacted = true;
        TimeManager.dayNumber++;

        Debug.Log("Day number: " + TimeManager.dayNumber);

        TimeManager.callNumber = 0;
        if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadSceneWithFade(Scene);
        }
        else
        {
            SceneManager.LoadScene(Scene);
        }
    }
}
