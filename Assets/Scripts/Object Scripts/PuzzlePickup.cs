using UnityEngine;

public class PuzzlePickup : MonoBehaviour
{
    Rigidbody2D rb;
    private bool held = false;
    private Vector3 mouseOffset;
    private Camera mainCam;
    private void Start()
    {
        mouseOffset = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        //gameObject.SetActive(false);
        mainCam = FindObjectOfType<Camera>().GetComponent<Camera>();
    }
    private void OnMouseDown()
    {
        rb.gravityScale = 0;
        held = true;
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseOffset = transform.position - mouseWorldPos;
    }

    private void OnMouseUp()
    {
        rb.gravityScale = 1;
        held=false;
        mouseOffset = Vector3.zero;
    }
    private void FixedUpdate()
    {
        if (held)
        {
            rb.MovePosition(mainCam.ScreenToWorldPoint(Input.mousePosition)+mouseOffset);
        }
    }

}
