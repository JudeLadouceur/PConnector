using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[NodeInfo("Scene", "Scenes/Scene", hasMultipleFlowInputs:true, hasMultipleFlowOutputs: true)]
public class SceneNode : CodeGraphNode
{
#if UNITY_EDITOR
    [ExposedProperty()]
    public SceneAsset scene;
    public override void OnBeforeSerialize()
    {
        base.OnBeforeSerialize();
        if (scene == null) return;
        if (sceneName != scene.name)
        {
            sceneName = scene.name;
            Debug.Log(sceneName);
        }
            
        
    }
#endif

    public string sceneName;

    public override void OnProcess()
    {
        Debug.Log("Going to: " + sceneName);

        if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadSceneWithFade(sceneName);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
