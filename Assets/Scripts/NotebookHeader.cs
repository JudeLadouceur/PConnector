using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotebookHeader : MonoBehaviour
{
    public static NotebookHeader instance;

    private TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateHeader()
    {
        text.text = ("Day " + (TimeManager.dayNumber + 1));
    }

    public void UpdateHeader(string headerText)
    {
        text.text = headerText;
    }
}
