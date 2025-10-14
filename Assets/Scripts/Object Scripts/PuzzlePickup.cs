using UnityEngine;

public class PuzzlePickup : MonoBehaviour
{
    Rigidbody2D rb;
    private bool held = false;
    private Vector3 mouseOffset;
    private Camera mainCam;
    Achievement achieve;
    private void Start()
    {
        mouseOffset = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        mainCam = FindObjectOfType<Camera>().GetComponent<Camera>();
        GetComponent<SpriteRenderer>().color = Color.green;
        achieve = GetComponentInParent<Achievement>();
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
        if (achieve.rightPiece)
        {
            achieve.SetStatus(AchievementStatus.Placed);
        }
    }
    private void FixedUpdate()
    {
        if (held)
        {
            rb.MovePosition(mainCam.ScreenToWorldPoint(Input.mousePosition)+mouseOffset);
        }
    }

}
