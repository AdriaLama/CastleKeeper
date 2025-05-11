using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float movementThreshold = 0.1f;

    private Rigidbody2D rb;
    private MovimientoPJ movimientoPJ;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        movimientoPJ = GetComponentInParent<MovimientoPJ>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > movementThreshold && movimientoPJ != null && movimientoPJ.checkGroundLineCast())
        {
            if (!audioSource.isPlaying)
            {
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
