using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeInfo("Debug Log", "Debug/Debug Log Console")]
public class DebugLogNode : CodeGraphNode
{
    [ExposedProperty()]
    public string logMessage;

    public override void OnProcess()
    {
        Debug.Log(logMessage);
    }

    public override void SetUniqueVariables()
    {
        m_continueAfterProcess = true;
    }
}
