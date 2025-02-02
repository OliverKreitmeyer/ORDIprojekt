using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{

    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if(isLadder == true && Math.Abs(vertical) > 0f)
        {
            isClimbing = true;
            rb.velocity = new Vector2(0,0);

        }
    }

    private void FixedUpdate()
    {
        if (isClimbing == true)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
             isLadder = true;
             
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
             isLadder = false;
             isClimbing = false;
        }
    }

}
