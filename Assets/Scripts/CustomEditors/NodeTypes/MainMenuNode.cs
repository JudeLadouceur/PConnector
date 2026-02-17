using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[NodeInfo("Main Menu", "Scenes/Main Menu", hasFlowInput:false, hasMultipleFlowOutputs:true)]
public class MainMenuNode : CodeGraphNode
{
    [ExposedProperty()]
    public SceneAsset scene;

    public override void OnProcess()
    {
        Debug.Log("Going to: Main Menu.");

        if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadSceneWithFade(scene.name);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
        }
    }
}
