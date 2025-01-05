using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzlePlayerController : MonoBehaviour
{
    public Transform movepoint;
    private Vector2 movement;
    public LayerMask stopsMovement;
    private Rigidbody2D MovePointRb;

    private void Awake() {
        movepoint.parent = null;
        MovePointRb = movepoint.GetComponent<Rigidbody2D>();
    }
    
    /*
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, movepoint.position, 5 * Time.deltaTime);

        

        if (Vector2.Distance(transform.position, movepoint.position) <= .05f)
        {
            if (Mathf.Abs(movement.x) == 1f)
            {
            //movepoint.position += new Vector3(movement.x, 0f, 0f);
            MovePointRb.MovePosition(MovePointRb.position + movement * 15 * Time.fixedDeltaTime);
            }
            else if (Mathf.Abs(movement.y) == 1f)
            {
            //movepoint.position += new Vector3(0f, movement.y, 0f);
            MovePointRb.MovePosition(MovePointRb.position + movement * Time.fixedDeltaTime);
            }
        }
    }
    */
    
    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position,movepoint.position, 5 * Time.deltaTime);

        if(Vector2.Distance(transform.position,movepoint.position) <= .05f) {
            if (Mathf.Abs(movement.x) == 1f) {
                if(!Physics2D.OverlapCircle(movepoint.position + new Vector3(movement.x, 0f, 0f), .2f, stopsMovement)) {
                    movepoint.position += new Vector3(movement.x, 0f, 0f);
                }
            } else if (Mathf.Abs(movement.y) == 1f) {
                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, movement.y, 0f), .2f, stopsMovement))
                {
                    movepoint.position += new Vector3(0f, movement.y, 0f);
                }
            }
        }
    }
    
    private void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
    }
}
