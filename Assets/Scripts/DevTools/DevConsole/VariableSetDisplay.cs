using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariableSetDisplay : MonoBehaviour
{
    public int selectedOption = 0;

    private TextMeshProUGUI text;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = ((DialogueVar)selectedOption).ToString();
    }

    public void ChangeDisplay(bool increasing)
    {
        if (increasing)
        {
            selectedOption++;
            if (selectedOption == VariableManager.instance.flags.Count) selectedOption = 0;
        }
        else
        {
            selectedOption--;
            if (selectedOption == -1) selectedOption = VariableManager.instance.flags.Count - 1;
        }
        text.text = ((DialogueVar)selectedOption).ToString();
    }
}
