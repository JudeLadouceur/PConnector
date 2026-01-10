using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{

    public string Scene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
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
