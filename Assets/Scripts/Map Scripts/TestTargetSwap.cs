using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestTargetSwap : MonoBehaviour
{
    public Transform[] targets;
    public string[] targetText;
    public ArrowPoint arrow;
    public TMPro.TextMeshProUGUI tmp;
    public MovementScript player;
    public bool lockoutObj = false;
    public int currentObjective = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetObjective(0);
            EndLockout();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetObjective(1);
            EndLockout();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetObjective(2);
            EndLockout();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetObjective(3);
            StartLockout();
        }
    }

    public void DisableArrow()
    {
        arrow.gameObject.SetActive(false);
    }
    public void EnableArrow()
    {
        arrow.gameObject.SetActive(true);
    }

    public void StartLockout()
    {
        lockoutObj = true;
        player.Funny = true;
    }

    public void EndLockout()
    {
        if (lockoutObj)
        {
            lockoutObj = false;
            player.Funny = false;
        }
        if(currentObjective == targets.Count() - 1)
        {
            SetObjective(0);
        }
        
    }

    public void SetObjective(int num)
    {
        currentObjective = num;
        if (targetText[num] != null)
        {
            tmp.text = targetText[num];
        }
        if (targets[num] != null)
        {
            EnableArrow();
            arrow.ChangeTarget(targets[num]);
        } else
        {
            DisableArrow();
        }
    }
}
