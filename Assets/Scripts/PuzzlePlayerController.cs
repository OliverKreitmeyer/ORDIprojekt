using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzlePlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private Transform movepoint;
    private Vector2 movement;
    private Vector3 playerDirection = new Vector3(1, 0, 0);
    [SerializeField] private LayerMask stopsMovement;
    [SerializeField] private LayerMask boxLayer;
    [SerializeField] private Animator playerAnimator;
    public bool hasKey=false;
    private GameObject pushedBox;
    private Vector3? pushedBoxMovepoint;

    private void Awake() {
        movepoint.parent = null;
    }
    
    private void Update() {

        //Player movement
        playerAnimator.SetFloat("xVelocity", Mathf.Abs(movement.x));
        playerAnimator.SetFloat("yVelocity", movement.y);

        transform.position = Vector2.MoveTowards(transform.position,movepoint.position, movementSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position,movepoint.position) <= .05f) {
            if (Mathf.Abs(movement.x) == 1f) {
                movement.Normalize();
                playerDirection = new Vector3(movement.x,movement.y);
                
                if (!(Physics2D.OverlapCircle(movepoint.position + new Vector3(movement.x, 0f, 0f), .2f, stopsMovement) ||
                        Physics2D.OverlapCircle(movepoint.position + new Vector3(movement.x, 0f, 0f), .2f, boxLayer)))
                {
                    movepoint.position += new Vector3(movement.x, 0f, 0f);
                }
            } else if (Mathf.Abs(movement.y) == 1f) {
                movement.Normalize();
                playerDirection = new Vector3(movement.x, movement.y);
                
                if (!(Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, movement.y, 0f), .2f, stopsMovement) ||
                        Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, movement.y, 0f), .2f, boxLayer)))
                {
                    movepoint.position += new Vector3(0f, movement.y, 0f);
                }
            }
        }

        //Pushing boxes
        if (pushedBox != null && pushedBoxMovepoint != null && Vector2.Distance(pushedBox.transform.position, (Vector3)pushedBoxMovepoint) != 0f)
        {
            pushedBox.transform.position = Vector2.MoveTowards(pushedBox.transform.position, (Vector3)pushedBoxMovepoint, movementSpeed * 3 * Time.deltaTime);
        }
        else
        {
            pushedBox = null;
            pushedBoxMovepoint = null;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //pokrenuti animaciju -> postaivit push parametar
            playerAnimator.SetTrigger("push");
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, playerDirection, 1f, boxLayer);
            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Boxes")) //promijeni da koristi boxlayer
            {
                GameObject box = hitInfo.collider.gameObject;
                if (!(Physics2D.OverlapCircle(box.transform.position + playerDirection, .2f, stopsMovement) ||
                        Physics2D.OverlapCircle(box.transform.position + playerDirection, .2f, boxLayer)))
                {
                    pushedBox = box;
                    pushedBoxMovepoint = box.transform.position + playerDirection;
                }      
            }
        }        
    }
    
    private void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
    }
}
