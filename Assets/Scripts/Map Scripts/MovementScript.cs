using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public bool Funny;
    public bool tutFunny;
    public bool achieveFunny = false;
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
        
        /*if (Input.GetKeyDown(KeyCode.Tab) && canToggle)
        {
            Funny = !Funny;
        }*/
        moveSpeed = Funny ? 0f : 5f;
        if (tutFunny) moveSpeed = 0;
        if (achieveFunny) moveSpeed = 0;
        if (Funny) return;
        if (tutFunny) return;
        if(achieveFunny) return;

        float moveX = 0;
        float moveY = 0;

        //If the mouse is down, use that for movement
        if (Input.GetMouseButton(0))
        {
            //Get the mouse's position and figure out the direction the mouse is from the character
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            //Debug.Log("Vector: " + target + " - Magnitude: " + target.magnitude);

            //If the mouse is too close to the player, don't move
            if (target.magnitude > 0.6f)
            {
                target = target.normalized;
                moveX = target.x;
                moveY = target.y;
                Debug.Log("X: " + moveX + " - Y: " + moveY);
            }
        }
        //If the mouse is not down, use the keyboard for movement
        else
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Tutorial World" && TestTargetSwap.instance != null && moveDirection.magnitude != 0)
        {
            TestTargetSwap.instance.AttemptProgress(0);
        }

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
