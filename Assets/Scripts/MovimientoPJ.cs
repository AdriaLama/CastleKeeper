using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPJ : MonoBehaviour
{
    private BoxCollider2D vx;
    private PickItems pi;
    private Vidas vi;
    public float movSpeed;
    public float horizontal;
    public float jumpSpeed;
    Rigidbody2D rb2D;
    private float baseGravity;
    private bool canDoubleJump;
    public float dashingTime;
    public float dashSpeed;
    public float dashCD;
    private bool canDash = true;
    private bool isDashing;
    public bool isRight;
    public bool isLeft;
    public float jumpWallx;
    public float jumpWally;
    private bool isTouchingWall;
    private bool isWallJumping;
    public float wallJumpTime = 0.2f;
    public float movSpeedDefault;
   

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        baseGravity = rb2D.gravityScale;
        vx = GetComponent<BoxCollider2D>();
        pi = GetComponent<PickItems>();
        vi = FindObjectOfType<Vidas>();
        movSpeedDefault = movSpeed;
    }

    void Update()
    {
        
        if (!isWallJumping)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        if (!checkGroundLineCast())
        {
            movSpeed = 6;
        }
        else
        {
            movSpeed = movSpeedDefault;
        }
      

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
            WallJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing && !isWallJumping)
        {
            Move();
        }
    }

    private bool checkGroundLineCast()
    {   
        
       return Physics2D.Linecast(transform.position + Vector3.down * 1.05f, transform.position + Vector3.down * 1.25f);

        
    }

    private void Move()
    {
        rb2D.velocity = new Vector2(horizontal * movSpeed, rb2D.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            
              if (checkGroundLineCast())
              {
                    canDoubleJump = true;
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
              }
              else if (canDoubleJump)
              {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                canDoubleJump = false;
              }

        }
    }

    private void WallJump()
    {
        
        if (isTouchingWall && !checkGroundLineCast() && pi.hasGrab && Input.GetButtonDown("Jump"))
        {
            isWallJumping = true;
            isTouchingWall = false;
            rb2D.velocity = new Vector2(jumpWallx * -horizontal, jumpWally);
            StartCoroutine(DisableWallJumping());
        }
    }

    private IEnumerator DisableWallJumping()
    {
        yield return new WaitForSeconds(wallJumpTime);
        isWallJumping = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && pi.hasGrab)
        {
            isTouchingWall = true;
            PhysicsMaterial2D newMaterial = new PhysicsMaterial2D() { friction = 5f };
            vx.sharedMaterial = newMaterial;
        }

        if (collision.gameObject.CompareTag("Pinchos"))
        {
           vi.vidasPlayer = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall") && pi.hasGrab && horizontal == -1 || collision.gameObject.CompareTag("Wall") && pi.hasGrab && horizontal == 1)
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
            PhysicsMaterial2D newMaterial = new PhysicsMaterial2D() { friction = 0f };
            vx.sharedMaterial = newMaterial;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.down * 1.05f, transform.position + Vector3.down * 1.25f);
    }
}