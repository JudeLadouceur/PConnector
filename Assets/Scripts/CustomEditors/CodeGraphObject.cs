using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeGraphObject : MonoBehaviour
{
    [SerializeField]
    private SceneManager m_graphAsset;

    private SceneManager graphInstance;

    private void OnEnable()
    {
        graphInstance = Instantiate(m_graphAsset);
        Debug.Log("Instantiated graph");
        ExecuteAsset();
    }

    private void ExecuteAsset()
    {
        CodeGraphNode mainMenuNode = graphInstance.GetMainMenuNode();

        graphInstance.Init(gameObject, mainMenuNode);
    }

    public void GoToNextScene()
    {
        graphInstance.GoToNextScene();
    }
}
