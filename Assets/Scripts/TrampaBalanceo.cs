using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaBalanceo : MonoBehaviour
{
    Rigidbody2D rb;
    public float leftLimit;
    public float rightLimit;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = 500;
    }

    // Update is called once per frame
    void Update()
    {
        swingMove();
    }

    void swingMove()
    {
        if(transform.rotation.z < rightLimit && rb.angularVelocity > 0 && rb.angularVelocity < speed)
        {
            rb.angularVelocity = speed;
        }
        else if(transform.rotation.z > leftLimit && rb.angularVelocity > 0 && rb.angularVelocity > -speed)
        {
            rb.angularVelocity = -speed;
        }
    }
}
