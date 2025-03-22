using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoPJ : MonoBehaviour
{

    public float movSpeed;
    public float horizontal;
    public float jumpSpeed;
    Rigidbody2D rb2D;
    private hook hk;
    private float baseGravity;
    private bool canDoubleJump;
    public float dashingTime;
    public float dashSpeed;
    public float dashCD;
    private bool canDash = true;
    private bool isDashing;
    public bool isRight;
    public bool isLeft;
   

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        baseGravity = rb2D.gravityScale;
       
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0)
        {
            isRight = true;
            isLeft = false;
        }
        if (horizontal < 0)
        {
            isLeft = true;
            isRight = false; 
        }

        if (!isDashing)
        {
            Jump();
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }

    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }

    }

    private void Move()
    {
        rb2D.velocity = new Vector2(horizontal * movSpeed, rb2D.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (checkGround.isGround)
            {
                canDoubleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            }
            else if (Input.GetButtonDown("Jump") && canDoubleJump)
            {

                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                canDoubleJump = false;

            }
        }
    }

    private IEnumerator Dash()
    {

        if (horizontal != 0 && canDash)
        {
            isDashing = true;
            canDash = false;
            rb2D.gravityScale = 0f;
            rb2D.velocity = new Vector2(horizontal * dashSpeed, 0f);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            rb2D.gravityScale = baseGravity;
            yield return new WaitForSeconds(dashCD);
            canDash = true;
        }

    }


}

