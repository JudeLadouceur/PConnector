using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public bool Funny;

    private Vector2 moveDirection;

    // Delay control
    private bool canToggle = true;
    public float toggleCooldown = 0.3f; // seconds between toggles

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Tab) && canToggle)
        {
            Funny = !Funny;

            moveSpeed = Funny ? 0f : 5f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
