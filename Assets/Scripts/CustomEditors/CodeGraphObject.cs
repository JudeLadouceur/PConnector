using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeGraphObject : MonoBehaviour
{
    [SerializeField]
    private CodeGraphAsset m_graphAsset;

    private CodeGraphAsset graphInstance;

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
