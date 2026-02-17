using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneByName : MonoBehaviour
{
    public string nameOfSceneToLoad;

    public void LoadSceneByName()
    {
        if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadSceneWithFade(nameOfSceneToLoad);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nameOfSceneToLoad);
        }
    }
}
