using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public int stepIndex;

    public void TransmitIndex()
    {
        TestTargetSwap.instance.AttemptProgress(stepIndex);
    }
}
