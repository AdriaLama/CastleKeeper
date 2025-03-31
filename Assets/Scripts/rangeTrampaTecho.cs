using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeTrampaTecho : MonoBehaviour
{
    public trampaTecho tr;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            tr.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tr.SetPlayerInRange(false);
        }
    }
}
