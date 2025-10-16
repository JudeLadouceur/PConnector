using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private CallManager callManager;

    public AudioSource audioSource;
    public AudioClip grabbingWireSound;
    public AudioClip connectingWireSound;

    private void Start()
    {
        callManager = GameObject.FindObjectOfType<CallManager>();
    }

    //When a notch is pressed, either start drawing a line or finish the line
    public void SelectPoint(Vector2 position, GameObject notch)
    {
        if (!hasPoint1)
        {
            StartDraw(position, notch);

            audioSource.PlayOneShot(grabbingWireSound);
        }
        else
        {
            EndDraw(position, notch);

            audioSource.PlayOneShot(connectingWireSound);
        }
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
        SO_Character target = (notch.GetComponent<Notches>().assignedCharacter);

        print(target);

        if (target == null)
        {
            Debug.LogError("There is no character assigned to this notch. Please assign someone by opening the Switchboard object, opening the notches variable, and assigning a character ScriptableObject to their character field. (instructions for creating a character ScriptableObject are in Assets -> Characters).");
            return;
        }

        print("start call");

        callManager.StartCall(notch.GetComponent<Notches>().assignedCharacter);

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