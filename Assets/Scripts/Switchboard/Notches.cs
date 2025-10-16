using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notches : MonoBehaviour
{
    private DrawLine lm;
    [HideInInspector]
    public bool isOccupied = false;

    public SO_Character assignedCharacter;

    private void Start()
    {
        lm = GameObject.Find("LineManager").GetComponent<DrawLine>();
    }

    private void OnMouseDown()
    {
        if (isOccupied) return;
        lm.SelectPoint(gameObject);
        isOccupied = true;
    }
}
