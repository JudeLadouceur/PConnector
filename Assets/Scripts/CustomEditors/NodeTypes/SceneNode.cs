using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[NodeInfo("Scene", "Scenes/Scene", hasMultipleFlowInputs:true, hasMultipleFlowOutputs: true)]
public class SceneNode : CodeGraphNode
{
    [ExposedProperty()]
    public SceneAsset scene;

    public override void OnProcess()
    {
        Debug.Log("Going to: " + scene.name);

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
