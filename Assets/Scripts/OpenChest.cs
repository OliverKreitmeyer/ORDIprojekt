using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private PuzzlePlayerController ppc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            ppc = collision.gameObject.GetComponent<PuzzlePlayerController>();
            if (ppc.hasKey)
            {
                
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                sr.color = Color.cyan;
            }
        }
    }
}
