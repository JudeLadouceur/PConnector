using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariableValDisplay : MonoBehaviour
{
    public int value = 0;

    private TextMeshProUGUI text;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = value.ToString();
    }

    public void ChangeDisplay(bool increasing)
    {
        if (increasing) value++;
        else value--;
        text.text = value.ToString();
    }
}
