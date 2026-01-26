using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public bool Funny;
    private Animator playerAnim;

    private Vector2 moveDirection;

    public static MovementScript instance;

    // Delay control
    public bool canToggle = true;
    public float toggleCooldown = 0.3f; // seconds between toggles

    private void Start()
    {
        instance = this;
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab) && canToggle)
        {
            Funny = !Funny;
        }
        moveSpeed = Funny ? 0f : 5f;
        if (Funny) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        //Animation stuff
        playerAnim.SetBool("IsMove", moveDirection.magnitude != 0);
        playerAnim.SetFloat("XMove", moveX);
        playerAnim.SetFloat("YMove", moveY);
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
