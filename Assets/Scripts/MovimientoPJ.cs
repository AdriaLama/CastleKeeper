using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPJ : MonoBehaviour
{

    public float movSpeed;
    public float horizontal;
    public float jumpSpeed;
    Rigidbody2D rb2D;
    private bool canDoubleJump;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontal, 0, 0) * movSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            if (checkGround.isGround)
            {
                canDoubleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            }
            else
            {
                if (Input.GetButtonDown("Jump"))
                {
                    if (canDoubleJump)
                    {
                        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                        canDoubleJump = false;
                    }
                }
            }
        }

        
    }
}
