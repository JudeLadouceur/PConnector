using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapInteractableTest : Interactables
{
    public GameObject Object;
    private float timePassed = 0f;
    public float speed;
    private Vector3 startPosition;
    private bool Interacts;
    public float swapDelay;
    private bool swapped = false;
    public int loopsCount;
    private int currentloop;
    void Start()
    {
        startPosition = Object.transform.position;
        loopsCount = loopsCount * 2;
    }

    private void Update()
    {
       if (Interacts == true)
        {
            float step = Time.deltaTime;
    
            if (swapped)
             {
                Object.transform.position += new Vector3(speed,0);
                timePassed += Time.deltaTime;

              }
            else
            {
                Object.transform.position += new Vector3(-speed, 0);
                timePassed += Time.deltaTime;
            }
            if (timePassed > swapDelay)
            {
                swapped = !swapped;
                currentloop++;
                timePassed = 0f;
               if(loopsCount == currentloop)
                {
                    Interacts = false;
                    currentloop = 0;
                    Object.transform.position = startPosition;
                }
            }
            //transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
        }
        
    }
    public override void Interact()
    {
        Interacts = true;
        Debug.Log("Interact");
    }
}
