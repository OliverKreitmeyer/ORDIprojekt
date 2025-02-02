using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            PuzzlePlayerController ppc = other.GetComponent<PuzzlePlayerController>();
            ppc.hasKey = true;
            doorAnimator.SetTrigger("keyPickUp");
        }
    }
}
