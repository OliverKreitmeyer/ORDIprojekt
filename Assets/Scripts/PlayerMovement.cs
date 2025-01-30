using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool isOnTheFloor = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        FlipController();

        if (Input.GetButtonDown("Jump") && isOnTheFloor)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isOnTheFloor = false;
            animator.SetBool("isJumping", !isOnTheFloor);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
        animator.SetFloat("xVelo", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelo", rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,0.2f,groundLayer);
    }

    private void FlipController()
    {
        if (rb.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        animator.transform.Rotate(0,180,0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnTheFloor = true;
        animator.SetBool("isJumping", !isOnTheFloor);
    }
}

