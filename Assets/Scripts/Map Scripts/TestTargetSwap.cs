using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetSwap : MonoBehaviour
{
    public Transform[] targets;
    public ArrowPoint arrow;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            arrow.ChangeTarget(targets[0]);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)){ arrow.ChangeTarget(targets[1]); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){ arrow.ChangeTarget(targets[2]); }
    }
}
