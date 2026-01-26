using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : Interactables
{

    public string Scene;

    public override void Interact()
    {
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
