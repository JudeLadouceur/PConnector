using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconBlink : MonoBehaviour
{
    public Image icon;
    public float blinkDuration;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > blinkDuration)
        {
            timer = 0;
            icon.enabled = !icon.enabled;
        }
    }
}
