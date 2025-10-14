using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehavior : MonoBehaviour
{
    private bool isMoving = true;
    private DrawLine lm;
    [HideInInspector]
    public GameObject _notch1;
    [HideInInspector]
    public GameObject _notch2;

    private void Start()
    {
        lm = GameObject.Find("LineManager").GetComponent<DrawLine>();
    }

    void Update()
    {
        if (!isMoving) return;
        Vector3 mouse = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);

        //Rotate towards the mouse
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Scale just slightly less than enough to reach the mouse (so that the collider of the wire doesn't overlap the notch's collider)
        transform.localScale = new Vector2(transform.localScale.x, lookDir.magnitude * 0.99f);
    }

    public void FinishMoving(Vector3 endPos, GameObject notch1, GameObject notch2)
    {
        //Rotate towards the ending
        Vector2 lookDir = endPos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Scale just enough to reach the mouse
        transform.localScale = new Vector2(transform.localScale.x, lookDir.magnitude);
        isMoving = false;

        _notch1 = notch1;
        _notch2 = notch2;

        lm.lineDrawn = true;
        this.enabled = false;
    }

    public void OnMouseDown()
    {
        //If no line is being made and this line is clicked, destroy it
        if (!lm.IsDrawing())
        {
            _notch1.GetComponent<Notches>().isOccupied = false;
            _notch2.GetComponent<Notches>().isOccupied = false;
            lm.lineDrawn = false;
            Destroy(gameObject);
        }
    }
}
