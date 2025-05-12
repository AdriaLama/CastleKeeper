using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoPJ : MonoBehaviour
{
    private BoxCollider2D vx;
    private PickItems pi;
    private Vidas vi;
    private Hook hk;
    public SpriteRenderer sr;
    public Animator animator;
    public float movSpeed;
    public float horizontal;
    public float jumpSpeed;
    public Rigidbody2D rb2D;
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
    private bool isWallJumping = false;
    public float wallJumpTime;
    public float movSpeedDefault;
    public bool tutorial;
    public bool bossDoor;
    public bool isKnock = false;
    private bool wasInAir = false;

   
    private PlayerSoundController soundController;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        baseGravity = rb2D.gravityScale;
        vx = GetComponent<BoxCollider2D>();
        pi = GetComponent<PickItems>();
        vi = FindObjectOfType<Vidas>();
        sr = GetComponent<SpriteRenderer>();
        hk = GetComponent<Hook>();
        animator = GetComponent<Animator>();
        movSpeedDefault = movSpeed;

        soundController = GetComponent<PlayerSoundController>();

        if (SceneManager.GetActiveScene().name == "PruebaJefe")
        {
            transform.position = new Vector2(-9.63f, 16.7f);
        }
    }

    void Update()
    {
        

        if (!isWallJumping && !hk.isGrappling)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        if (!checkGroundLineCast())
        {
            wasInAir = true; 
            movSpeed = 6;
        }

        else
        {
            movSpeed = movSpeedDefault;

  
            if (wasInAir)
            {
                animator.SetBool("Jump", false);
                wasInAir = false;
            }
        }

        if (horizontal > 0)
        {
            isRight = true;
            isLeft = false;
            sr.flipX = false;
        }
        if (horizontal < 0)
        {
            isLeft = true;
            isRight = false;
            sr.flipX = true;
        }

        if (!isDashing && !isKnock)
        {
            Jump();
            WallJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isKnock)
        {
            StartCoroutine(Dash());
        }

        if (horizontal == 0)
        {
            animator.SetBool("Walk", false);
        }

        if (vi.playerHit && vi.vidasPlayer >= 1)
        {
            StartCoroutine(Knockback(0.6f, 5f, 3f));
            vi.playerHit = false;
        }

        DoorTutorial();
        BossDoor();
    }

    private void FixedUpdate()
    {
        if (!isDashing && !isWallJumping && !isKnock)
        {
            Move();
        }
    }

    private void Move()
    {
        rb2D.velocity = new Vector2(horizontal * movSpeed, rb2D.velocity.y);
        animator.SetBool("Walk", true);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jump", true);
            if (checkGroundLineCast())
            {
                canDoubleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                animator.SetBool("Jump", true);

            }
            else if (canDoubleJump)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                canDoubleJump = false;
                StartCoroutine(ResetJumpAnimation());


            }
        }

        
        
        
    }

    private void WallJump()
    {
        if (!checkGroundLineCast() && pi.hasGrab && Input.GetButtonDown("Jump"))
        {
            if (checkRightWallLineCast() && !isWallJumping)
            {
                rb2D.velocity = new Vector2(-jumpWallx, jumpWally);
                sr.flipX = false;
                isWallJumping = true;
                StartCoroutine(DisableWallJumping());
            }
            else if (checkLeftWallLineCast() && !isWallJumping)
            {
                rb2D.velocity = new Vector2(jumpWallx, jumpWally);
                sr.flipX = true;
                isWallJumping = true;
                StartCoroutine(DisableWallJumping());
            }
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

            soundController?.PlayDashSound(); 

            rb2D.gravityScale = 0f;
            rb2D.velocity = new Vector2(horizontal * dashSpeed, 0f);

            yield return new WaitForSeconds(dashingTime);

            isDashing = false;
            rb2D.gravityScale = baseGravity;

            yield return new WaitForSeconds(dashCD);
            canDash = true;
        }
    }

    public void DoorTutorial()
    {
        if (tutorial)
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
    }
    public void BossDoor()
    {
        if (bossDoor)
        {
            SceneManager.LoadScene("PruebaJefe");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoorTutorial"))
        {
            tutorial = true;
        }
        if (collision.gameObject.CompareTag("BossDoor"))
        {
            bossDoor = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pinchos"))
        {
            vi.vidasPlayer = 0;
        }
    }


    private IEnumerator Knockback(float duration, float powerX, float powerY)
    {
        float timer = 0;
        isKnock = true;

        if (sr.flipX)
        {
            rb2D.velocity = new Vector2(powerX, powerY);
        }
        else
        {
            rb2D.velocity = new Vector2(-powerX, powerY);
        }

        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        isKnock = false;
    }

    public bool checkGroundLineCast()
    {
        RaycastHit2D[] hit1 = Physics2D.LinecastAll(transform.position + Vector3.down * 1f + Vector3.right * 0.80f, transform.position + Vector3.right * 0.80f + Vector3.down * 1.75f);
        RaycastHit2D[] hit2 = Physics2D.LinecastAll(transform.position + Vector3.down * 1f + Vector3.left * 0.80f, transform.position + Vector3.left * 0.80f + Vector3.down * 1.75f);
        RaycastHit2D[] hit3 = Physics2D.LinecastAll(transform.position + Vector3.down * 1f, transform.position + Vector3.down * 1.75f);

        foreach (RaycastHit2D hit in hit1)
        {
            if (hit.collider.CompareTag("Floor")) return true;
        }
        foreach (RaycastHit2D hit in hit2)
        {
            if (hit.collider.CompareTag("Floor")) return true;
        }
        foreach (RaycastHit2D hit in hit3)
        {
            if (hit.collider.CompareTag("Floor")) return true;
        }

        return false;
    }

    private bool checkRightWallLineCast()
    {
        RaycastHit2D[] hitsTop = Physics2D.LinecastAll(transform.position, transform.position + Vector3.right * 0.90f);
        RaycastHit2D[] hitsBottom = Physics2D.LinecastAll(transform.position + Vector3.down * 0.80f, transform.position + Vector3.down * 0.80f + Vector3.right * 0.90f);

        foreach (RaycastHit2D hit in hitsTop)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                vx.sharedMaterial = new PhysicsMaterial2D() { friction = 5f };
                return true;
            }
        }

        foreach (RaycastHit2D hit in hitsBottom)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                vx.sharedMaterial = new PhysicsMaterial2D() { friction = 5f };
                return true;
            }
        }

        return false;
    }

    private bool checkLeftWallLineCast()
    {
        RaycastHit2D[] hitsTop = Physics2D.LinecastAll(transform.position, transform.position + Vector3.left * 0.90f);
        RaycastHit2D[] hitsBottom = Physics2D.LinecastAll(transform.position + Vector3.down * 0.80f, transform.position + Vector3.down * 0.80f + Vector3.left * 0.90f);

        foreach (RaycastHit2D hit in hitsTop)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                vx.sharedMaterial = new PhysicsMaterial2D() { friction = 5f };
                return true;
            }
        }

        foreach (RaycastHit2D hit in hitsBottom)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                vx.sharedMaterial = new PhysicsMaterial2D() { friction = 5f };
                return true;
            }
        }

        return false;
    }

    private IEnumerator ResetJumpAnimation()
    {
        animator.SetBool("Jump", false); 
        yield return null;               
        animator.SetBool("Jump", true);  
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.down * 1f + Vector3.right * 0.80f, transform.position + Vector3.right * 0.80f + Vector3.down * 1.75f);
        Gizmos.DrawLine(transform.position + Vector3.down * 1f + Vector3.left * 0.80f, transform.position + Vector3.left * 0.80f + Vector3.down * 1.75f);
        Gizmos.DrawLine(transform.position + Vector3.down * 1f, transform.position + Vector3.down * 1.75f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 0.90f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 0.90f);
        Gizmos.DrawLine(transform.position + Vector3.down * 0.80f, transform.position + Vector3.down * 0.80f + Vector3.right * 0.90f);
        Gizmos.DrawLine(transform.position + Vector3.down * 0.80f, transform.position + Vector3.down * 0.80f + Vector3.left * 0.90f);
    }
}
