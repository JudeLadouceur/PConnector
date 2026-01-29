using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TutorialStep
{
    public GameObject arrowTarget;
    public string stepText;
    public bool lockout;
    public GameObject[] enableComponents;
    public Button[] enableButtons;
    public GameObject[] disableComponents;
    public Button[] disableButtons;
    public string stepSummary;
}
