using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaula : MonoBehaviour
{
    private Rigidbody2D rb;
    private RangeAttack ra;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ra = FindObjectOfType<RangeAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ra.JaulaFall)
        {
            rb.gravityScale = 1;
        }
    }
}
