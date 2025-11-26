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
        SceneManager.LoadScene(Scene);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(Scene);
    }

    public void NextDay()
    {
        TimeManager.dayNumber++;

        Debug.Log("Day number: " + TimeManager.dayNumber);

        TimeManager.callNumber = 0;
        SceneManager.LoadScene(Scene);
    }
}
