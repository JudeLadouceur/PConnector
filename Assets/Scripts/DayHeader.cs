using TMPro;
using UnityEngine;

public class DayHeader : MonoBehaviour
{
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        text.text = ("Day " + (TimeManager.dayNumber + 1) + " of 5");
    }
}