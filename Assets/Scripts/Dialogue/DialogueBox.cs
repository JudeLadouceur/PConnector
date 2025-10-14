using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float spriteDimensions = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        transform.localScale = new Vector2(Camera.main.scaledPixelWidth * 0.8f, Camera.main.scaledPixelHeight * 0.4f);
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3 (0, Camera.main.scaledPixelHeight * -0.2f);
    }
}
