using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetToFloor2 : MonoBehaviour
{
    private PuzzlePlayerController ppc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ppc = collision.gameObject.GetComponent<PuzzlePlayerController>();
            if (ppc.hasKey)
                SceneManager.LoadScene("Floor 1");
        }
    }
}
