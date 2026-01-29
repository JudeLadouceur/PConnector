using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TestTargetSwap : MonoBehaviour
{
    public TutorialStep[] steps;
    public ArrowPoint arrow;
    public TMPro.TextMeshProUGUI tmp;
    public MovementScript player;
    public int currentStep = 0;
    private KeyCode[] keys;

    private void Start()
    {
        keys = new KeyCode[] {
            KeyCode.Alpha0,
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9
            };
        SetObjective(0);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < keys.Count(); i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    SetObjective(i);
                }
            }
        }
#endif
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
        player.Funny = true;
    }

    public void EndLockout()
    {
        if (steps[currentStep].lockout)
        {
            player.Funny = false;
        }

        /*if(currentStep == steps.Count() - 1)
        {
            SetObjective(0);
        }*/

    }

    public void SetObjective(int num)
    {
        if (num >= steps.Count())
            return;
        currentStep = num;
        if (steps[currentStep].stepText != null)
        {
            tmp.text = steps[currentStep].stepText;
        }
        if (steps[currentStep].arrowTarget != null)
        {
            EnableArrow();
            arrow.ChangeTarget(steps[currentStep].arrowTarget.transform);
        }
        else
        {
            DisableArrow();
        }
        if (steps[currentStep].lockout)
        {
            StartLockout();
        }
        else
        {
            EndLockout();
        }
        foreach (GameObject script in steps[currentStep].enableComponents)
        {
            script.SetActive(true);
        }
        foreach (GameObject script in steps[currentStep].disableComponents)
        {
            script.SetActive(false);
        }
    }
}
