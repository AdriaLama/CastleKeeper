using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaula : MonoBehaviour
{
    private Rigidbody2D rb;
    private RangeAttack ra;
    private BoxCollider2D bx;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ra = FindObjectOfType<RangeAttack>();
        bx = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ra.JaulaFall)
        {
            rb.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            bx.enabled = !bx.enabled;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
