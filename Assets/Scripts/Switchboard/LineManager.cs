using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject line;

    public float lineThickness;

    private GameObject currentLine;

    private bool hasPoint1 = false;
    [HideInInspector]
    public GameObject notch1;

    [HideInInspector]
    public bool lineDrawn = false;

    private CallManager callManager;

    public AudioSource audioSource;
    public AudioClip grabbingWireSound;
    public AudioClip connectingWireSound;

    public static LineManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        callManager = GameObject.FindObjectOfType<CallManager>();
    }

    //When a notch is pressed, either start drawing a line or finish the line
    public void SelectPoint(GameObject notch)
    {
        if (!hasPoint1)
        {
            StartDraw(notch);

            audioSource.PlayOneShot(grabbingWireSound);
        }
        else
        {
            if (EndDraw(notch) == false)
            {
                //-----Insert invalid notch sound---------
                return;
            }

            
            audioSource.PlayOneShot(connectingWireSound);
        }
    }

    private void StartDraw(GameObject notch)
    {
        //If a line is already drawn, don't start another
        if (lineDrawn == true) return;

        Vector2 position = notch.transform.position;

        //Create the connection with the desired thickness at the center of the pressed notch
        currentLine = Instantiate(line, position, Quaternion.identity);
        currentLine.transform.localScale = new Vector2(lineThickness, 0);

        hasPoint1 = true;
        notch1 = notch;
    }

    private bool EndDraw(GameObject notch)
    {
        SO_Character target = (notch.GetComponent<Notches>().assignedCharacter);

        print(target);

        if (target == null)
        {
            Debug.LogError("There is no character assigned to this notch. Please assign someone by opening the Switchboard object, opening the notches variable, and assigning a character ScriptableObject to their character field. (instructions for creating a character ScriptableObject are in Assets -> Characters).");
            return false;
        }

        print("start call");

        callManager.StartCall(notch.GetComponent<Notches>().assignedCharacter);

        //Set the end point for the connection and stop it from moving
        currentLine.GetComponent<LineBehavior>().FinishMoving(notch.transform.position, notch1, notch);
        currentLine = null;

        hasPoint1 = false;

        return true;
    }

    public bool IsDrawing()
    {
        if (hasPoint1) return true;
        return false;
    }
}