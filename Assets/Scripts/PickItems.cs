using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItems : MonoBehaviour
{
    public bool hasHook;
    public bool hasGrab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hook"))
        {
            hasHook = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Grab"))
        {
            hasGrab = true;
            Destroy(collision.gameObject);
        }
    }

}
