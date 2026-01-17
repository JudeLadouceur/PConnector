using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetSwap : MonoBehaviour
{
    public Transform[] targets;
    public string[] targetText;
    public ArrowPoint arrow;
    public TMPro.TextMeshProUGUI tmp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arrow.ChangeTarget(targets[0]);
            tmp.text = targetText[0];
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arrow.ChangeTarget(targets[1]);
            tmp.text = targetText[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            arrow.ChangeTarget(targets[2]);
            tmp.text = targetText[2];
        }
    }
}
