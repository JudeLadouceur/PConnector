using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class CanvasFindCamera : MonoBehaviour
{
    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += FindCamera;
        FindCamera(UnityEngine.SceneManagement.SceneManager.GetActiveScene(), UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        
    }

    private void FindCamera(Scene scene1, Scene scene2)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= FindCamera;
    }
}
