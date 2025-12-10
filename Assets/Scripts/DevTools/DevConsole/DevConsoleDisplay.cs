using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DevConsoleDisplay : MonoBehaviour
{
    public string[] options;

    public int selectedOption = 0;

    private TextMeshProUGUI text;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = options[selectedOption];
    }

    public void ChangeDisplay(bool increasing)
    {
        if (increasing)    
        {
            selectedOption++;
            if (selectedOption == options.Length) selectedOption = 0;
        }
        else
        {
            selectedOption--;
            if (selectedOption == -1) selectedOption = options.Length - 1;
        }
        text.text = options[selectedOption];
    }
}
