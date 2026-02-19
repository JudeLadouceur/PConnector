using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[NodeInfo("Main Menu", "Scenes/Main Menu", hasFlowInput:false, hasMultipleFlowOutputs:true)]
public class MainMenuNode : CodeGraphNode
{
#if UNITY_EDITOR
    [ExposedProperty()]
    public SceneAsset scene;
    public override void OnBeforeSerialize()
    {
        base.OnBeforeSerialize();
        if (scene == null ) return;
        if (sceneName != scene.name)
            sceneName = scene.name;
    }
#endif
    public string sceneName;


    public override void OnProcess()
    {
        Debug.Log("Going to: Main Menu.");

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
