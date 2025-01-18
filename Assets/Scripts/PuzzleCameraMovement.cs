using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;


    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y <= 2)
        {
            if (transform.position != new Vector3(-5.5f, -2f, -10f))
            {
                transform.position = new Vector3(-5.5f, -2f, -10f);
                //transform.position = Vector2.MoveTowards(transform.position, new Vector3(-5.5f, -2f, -10f), 20 * Time.deltaTime);
                Camera.main.orthographicSize = 4;
            }
        }
        else if(player.transform.position.y >= 13)
        {
            if(transform.position!= new Vector3(-9f, 20f, -10f))
            {
                transform.position = new Vector3(-9f,20f,-10f);
                //transform.position = Vector2.MoveTowards(transform.position, new Vector3(-9f, 20f, -10f), 20 * Time.deltaTime);
                Camera.main.orthographicSize = 8;
            }
        }
        else
        {
            if (transform.position != new Vector3(-1f, 7f, -10f))
            {
                transform.position = new Vector3(-1f,7f,-10f);
                //transform.position = Vector2.MoveTowards(transform.position, new Vector3(-1f, 7f, -10f), 20 * Time.deltaTime);
                Camera.main.orthographicSize = 6;

            }
        }
    }
}
