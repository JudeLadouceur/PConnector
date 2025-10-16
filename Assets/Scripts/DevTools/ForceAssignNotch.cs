using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAssignNotch : MonoBehaviour
{
    public bool isActive = false;

    [Tooltip("if this tool is active it will read the calls and auto assign the first notch for each call to the corresponding notch here.")]
    public GameObject[] autoNotches;
}
