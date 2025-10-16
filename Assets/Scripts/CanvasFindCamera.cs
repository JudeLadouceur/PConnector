using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class CanvasFindCamera : MonoBehaviour
{
    private void Start()
    {
        SceneManager.activeSceneChanged += FindCamera;
        
    }

    private void FindCamera(Scene scene1, Scene scene2)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
