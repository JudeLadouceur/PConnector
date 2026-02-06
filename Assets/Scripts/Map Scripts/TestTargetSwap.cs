using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestTargetSwap : MonoBehaviour
{
    public static TestTargetSwap instance;
    public TutorialStep[] steps;
    public ArrowPoint arrow;
    public TMPro.TextMeshProUGUI tmp;
    public MovementScript player;
    public int currentStep = 0;
    private KeyCode[] keys;

    private void Start()
    {
        instance = this;
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
        if (steps[currentStep].lockout!=player.tutFunny)
        {
            player.tutFunny = steps[currentStep].lockout;
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
        player.tutFunny = true;
    }

    public void EndLockout()
    {
        if (steps[currentStep].lockout)
        {
            player.tutFunny = false;
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
        
        if (steps[num].stepText != null)
        {
            tmp.text = steps[num].stepText;
        }
        if (steps[num].arrowTarget != null)
        {
            EnableArrow();
            arrow.ChangeTarget(steps[num].arrowTarget.transform);
        }
        else
        {
            DisableArrow();
        }
        if (steps[num].lockout)
        {
            StartLockout();
        }
        else
        {
            EndLockout();
        }
        foreach (GameObject script in steps[num].enableComponents)
        {
            script.SetActive(true);
        }
        foreach (GameObject script in steps[num].disableComponents)
        {
            script.SetActive(false);
        }
        foreach (Button b in steps[num].enableButtons)
        {
            b.interactable = true;
        }
        foreach (Button b in steps[num].disableButtons)
        {
            b.interactable = false;
        }
        currentStep = num;
    }

    public void AttemptProgress(int i)
    {
        if (i == currentStep)
        {
            currentStep++;
            SetObjective(currentStep);
        }
    }
}
