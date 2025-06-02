using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaula : MonoBehaviour
{
    private Rigidbody2D rb;
    private RangeAttack ra;
    private BoxCollider2D bx;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip caidaClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ra = FindObjectOfType<RangeAttack>();
        bx = GetComponent<BoxCollider2D>();
    }

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
            if (audioSource != null && caidaClip != null)
            {
                audioSource.PlayOneShot(caidaClip);
            }

            bx.enabled = !bx.enabled;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
