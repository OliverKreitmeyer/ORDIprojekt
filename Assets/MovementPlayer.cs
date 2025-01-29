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
    private int remainingDoubleJumps;
    private float coyoteTime;
    private bool isGrounded;
    private bool canDoubleJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        float moveInput = Input.GetAxisRaw("Horizontal"); 
        Move(moveInput);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
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
    }

    private void Move(float moveInput)
    {
        // Move the player left or right
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the player sprite based on movement direction
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
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
}
