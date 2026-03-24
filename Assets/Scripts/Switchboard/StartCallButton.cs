using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCallButton : MonoBehaviour
{
    public void OnClick()
    {
        CallManager.instance.SkipCallDelay();
    }
}
