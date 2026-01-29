using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TutorialStep
{
    public GameObject arrowTarget;
    public string stepText;
    public bool lockout;
    public GameObject[] enableComponents;
    public GameObject[] disableComponents;
}
