using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzlePlayerController : MonoBehaviour
{
    public Transform movepoint;
    private Vector2 movement;
    public LayerMask stopsMovement; //mozda bolje kao integer
    public LayerMask boxLayer;
    private Vector3 playerDirection = new Vector3(1,0);

    private void Awake() {
        movepoint.parent = null;
    }
    
    private void Update() {
        
        //Player movement
        transform.position = Vector2.MoveTowards(transform.position,movepoint.position, 5 * Time.deltaTime);

        if(Vector2.Distance(transform.position,movepoint.position) <= .05f) {
            if (Mathf.Abs(movement.x) == 1f) {
                //rotate the player in the direction of movement
                movement.Normalize();
                playerDirection = new Vector3(movement.x,movement.y);
                //Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation,720 * Time.deltaTime);
                if(!(Physics2D.OverlapCircle(movepoint.position + new Vector3(movement.x, 0f, 0f), .2f, stopsMovement) ||
                        Physics2D.OverlapCircle(movepoint.position + new Vector3(movement.x, 0f, 0f), .2f, boxLayer))) {
                    movepoint.position += new Vector3(movement.x, 0f, 0f);
                }
            } else if (Mathf.Abs(movement.y) == 1f) {
                movement.Normalize();
                playerDirection = new Vector3(movement.x, movement.y);
                //Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
                if (!(Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, movement.y, 0f), .2f, stopsMovement) ||
                        Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, movement.y, 0f), .2f, boxLayer)))
                {
                    movepoint.position += new Vector3(0f, movement.y, 0f);
                }
            }
        }

        //Pushing boxes
        //raycastanje na duljinu od 1 i layer Boxes
        //ako kliknem space provjeri moze li se pomaknut kutija u tom smjeru gdje je i zraka poslana
        //ako moze pomakni ju
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, playerDirection, 1);

            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Boxes")) //promijeni da koristi boxlayer
            {
                Debug.Log("vektor smjera: "+ playerDirection.ToString());
                GameObject box = hitInfo.collider.gameObject;
                //provjeriti je li u tom smjeru za jedno mjesto zid ili nije -> ako nije pomaknut kutiju
                RaycastHit2D wallHit = Physics2D.Raycast(box.transform.position, playerDirection, 1);
                Debug.Log("wallhit: "+wallHit);
                if (!Physics2D.OverlapCircle(box.transform.position + playerDirection, .2f, stopsMovement)){
                    box.transform.position = box.transform.position + playerDirection;
                }
                
            }
        }
    }
    
    private void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
    }
    
}
