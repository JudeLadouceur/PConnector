using UnityEngine;

public class PuzzlePickup : MonoBehaviour
{
    Rigidbody2D rb;
    private bool held = false;
    private Vector3 mouseOffset;
    Achievement achieve;
    private void Start()
    {
        mouseOffset = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = Color.green;
        achieve = GetComponentInParent<Achievement>();
    }
    private void OnMouseDown()
    {
        rb.gravityScale = 0;
        held = true;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseOffset = transform.position - mouseWorldPos;
    }

    private void OnMouseUp()
    {
        rb.gravityScale = 1;
        held=false;
        mouseOffset = Vector3.zero;
        if (achieve.rightPiece)
        {
            achieve.SlotPiece();
        }
    }
    private void FixedUpdate()
    {
        if (held)
        {
            rb.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)+mouseOffset);
        }
    }

}
