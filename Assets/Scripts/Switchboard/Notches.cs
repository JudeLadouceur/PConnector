using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notches : MonoBehaviour
{
    [HideInInspector]
    public bool isOccupied = false;

    public SO_Character assignedCharacter;

    private void OnMouseDown()
    {
        if (isOccupied) return;
        LineManager.instance.SelectPoint(gameObject);
        isOccupied = true;
    }
}
