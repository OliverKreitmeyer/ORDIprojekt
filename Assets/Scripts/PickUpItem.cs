 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

    private Transform pickUpPoint;
    private Transform player;

    public float pickUpDistance;
    public float forceMulti;

    public bool readyToThrow;
    public bool itemIsPicked;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        pickUpPoint = GameObject.Find("PickUpPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && itemIsPicked == true && readyToThrow)
        {
            forceMulti += 300 * Time.deltaTime;
        }

        pickUpDistance = Vector2.Distance(player.position, transform.position);

        if (pickUpDistance < 2) 
        {
            if (Input.GetKeyDown(KeyCode.E) && itemIsPicked == false && pickUpPoint.childCount < 1)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<PolygonCollider2D>().enabled = false;
                this.transform.position = pickUpPoint.position;
                this.transform.parent = GameObject.Find("PickUpPoint").transform;

                itemIsPicked = true;
                forceMulti = 0;
            }
        }

        if (itemIsPicked)
        {
            this.transform.position = pickUpPoint.position;
        }

        if (Input.GetKeyUp(KeyCode.E) && itemIsPicked == true)
        {
            readyToThrow = true;

            if (forceMulti > 10)
            {
                rb.AddForce(player.transform.forward * forceMulti);
                this.transform.parent = null;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                GetComponent<PolygonCollider2D>().enabled = true;
                itemIsPicked = false;

                forceMulti = 0;
                readyToThrow = false;
            }

            forceMulti = 0;
        }
    }
}
