using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject line;

    public float lineThickness;

    private GameObject currentLine;

    private bool hasPoint1 = false;
    private GameObject notch1;

    [HideInInspector]
    public bool lineDrawn = false;

    //When a notch is pressed, either start drawing a line or finish the line
    public void SelectPoint(Vector2 position, GameObject notch)
    {
        if (!hasPoint1) StartDraw(position, notch);
        else EndDraw(position, notch);
    }

    private void StartDraw(Vector2 startPos, GameObject notch)
    {
        //If a line is already drawn, don't start another
        if (lineDrawn == true) return;

        Vector2 position = startPos;

        //Create the connection with the desired thickness at the center of the pressed notch
        currentLine = Instantiate(line, position, Quaternion.identity);
        currentLine.transform.localScale = new Vector2(lineThickness, 0);

        hasPoint1 = true;
        notch1 = notch;
    }

    private void EndDraw(Vector2 endPos, GameObject notch)
    {
        //Set the end point for the connection and stop it from moving
        currentLine.GetComponent<LineBehavior>().FinishMoving(endPos, notch1, notch);
        currentLine = null;

        hasPoint1 = false;
    }

    public bool IsDrawing()
    {
        if (hasPoint1) return true;
        return false;
    }
}
