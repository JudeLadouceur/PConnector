using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeInfo("Return To Main Menu", "Scenes/Return To Main Menu", hasFlowOutput: false)]
public class ReturnToMainMenuNode : CodeGraphNode
{
    public override void OnProcess()
    {
        SceneManager.instance.GoToScene(SceneManager.instance.GetMainMenuNode().id);
    }
}
