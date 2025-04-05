using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleVision : MonoBehaviour
{
    public Gargoyle gargoyle;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
           gargoyle.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gargoyle.SetPlayerInRange(false);
        }
    }
}
