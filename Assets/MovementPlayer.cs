using System;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float coyoteTimeDuration = 0.2f; 
    public int maxDoubleJumps = 1; 
    public LayerMask groundLayer; 

    [Header("References")]
    public Transform groundCheck; 

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int remainingDoubleJumps;
    private float coyoteTime;
    private bool isGrounded;
    private bool canDoubleJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        float moveInput = Input.GetAxisRaw("Horizontal"); 
        Move(moveInput);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            animator.SetBool("IsJumping", true);
        }
        if (isGrounded)
        {
            coyoteTime = coyoteTimeDuration;
            remainingDoubleJumps = maxDoubleJumps;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
        }

        HandleAnimations(moveInput);
    }

    private void Move(float moveInput)
    {
        // Move the player left or right
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the player sprite based on movement direction
        if (moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
    }

    private void Jump()
    {
        
        if (isGrounded || coyoteTime > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTime = 0f; 
            remainingDoubleJumps = maxDoubleJumps; 
        }
        else if (remainingDoubleJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            remainingDoubleJumps--;
        }
    }

     private void HandleAnimations(float moveInput)
    {
        // 🔥 Running Animation
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // 🔥 Falling Animation
        if (rb.velocity.y < -0.1f)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }

        // 🔥 Grappling Animation (if you have a grapple mechanic)
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Change "G" to your grapple key
        {
            animator.SetBool("IsGrappling", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("IsGrappling", false);
        }
    }
}
