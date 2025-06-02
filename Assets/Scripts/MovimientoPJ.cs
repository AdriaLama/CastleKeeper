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
    private KeyDoor kd;
    private tpPlayer tp;
    private DoorFinish df;
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
    public float coyoteTime;
    private float coyoteTimeCounter;
    public ParticleSystem particulas;
    public GameObject key;
    public bool bossTransition = false;
    private bool canPlayJumpSound = false;
    private float jumpInputLockTime = 0.2f; 
    private float jumpInputTimer = 0f;
    private bool jumpInputReleased = false;
    public bool puedeMoverse = true;



    [Header("Combat System")]
    public bool isAttacking = false;
    public int currentCombo = 0;
    public float comboWindow = 1.0f;
    public float attackCooldown = 0.1f;
    [Header("Combo Completion Cooldown")]
    public float comboCompleteCooldown;
    private bool isComboOnCooldown = false;
    private float lastAttackTime;
    private bool canAttack = true;
    private Coroutine comboResetCoroutine;
    private Coroutine comboCooldownCoroutine;

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
        kd = FindObjectOfType<KeyDoor>();
        df = FindObjectOfType<DoorFinish>();
        tp = GetComponent<tpPlayer>();
        movSpeedDefault = movSpeed;
        StartCoroutine(EnableJumpSoundDelay());
        jumpInputTimer = jumpInputLockTime;


        soundController = GetComponent<PlayerSoundController>();

        if (SceneManager.GetActiveScene().name == "PruebaJefe")
        {
            transform.position = new Vector2(-9.63f, 16.7f);
        }
<<<<<<< Updated upstream
       
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            transform.position = new Vector2(-28.42f, -4f);
        }
=======
        if (SceneManager.GetActiveScene().name == "Celda")
        {
            puedeMoverse = false;
        }

>>>>>>> Stashed changes
    }

    void Update()
    {
        if (!puedeMoverse)
        {
            horizontal = 0;
            animator.SetBool("Run", false);
            return;
        }


        if (!isWallJumping && !hk.isGrappling && tp.canMoveTp)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        if (!checkGroundLineCast())
        {
            wasInAir = true;
            movSpeed = 6;
            animator.SetBool("Grounded", false);
        }
        else
        {
            movSpeed = movSpeedDefault;
            animator.SetBool("Grounded", true);
            if (wasInAir)
            {
                animator.SetBool("Jump", false);
                wasInAir = false;
            }
        }

       
        if (horizontal > 0 && !isAttacking && !isWallJumping)
        {
            isRight = true;
            isLeft = false;
            sr.flipX = false;
        }
        if (horizontal < 0 && !isAttacking && !isWallJumping)
        {
            isLeft = true;
            isRight = false;
            sr.flipX = true;
        }

        if (!isDashing && !isKnock && !isAttacking)
        {
            Jump();
            WallJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isKnock && !isAttacking)
        {
            StartCoroutine(Dash());
        }

        HandleCombatInput();

        animator.SetFloat("AirSpeedY", rb2D.velocity.y);

        if (vi.playerHit && vi.vidasPlayer >= 1)
        {
            StartCoroutine(Knockback(0.6f, 5f, 3f));
            vi.playerHit = false;
        }

        if (jumpInputTimer > 0f)
        {
            jumpInputTimer -= Time.deltaTime;
        }


        DoorTutorial();
        BossDoor();
    }
    private IEnumerator EnableJumpSoundDelay()
    {
        yield return new WaitForSeconds(0.2f);
        canPlayJumpSound = true;
    }

    private void HandleCombatInput()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack && !isKnock && !isDashing && !isComboOnCooldown)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {

        if (comboResetCoroutine != null)
        {
            StopCoroutine(comboResetCoroutine);
        }


        if (Time.time - lastAttackTime > comboWindow)
        {
            currentCombo = 0;
        }


        currentCombo++;
        if (currentCombo > 3) currentCombo = 1;


        ResetAllAttackAnimations();


        switch (currentCombo)
        {
            case 1:
                ExecuteAttack1();
                break;
            case 2:
                ExecuteAttack2();
                break;
            case 3:
                ExecuteAttack3();
                break;
        }


        lastAttackTime = Time.time;
        canAttack = false;


        if (currentCombo == 3)
        {
            soundController?.PlayHeavyAttackSound();
            StartCoroutine(ComboCompleteCooldown());
        }
        else
        {
            soundController?.PlayAttackSound();
            comboResetCoroutine = StartCoroutine(ResetComboAfterDelay());
        }



        StartCoroutine(AttackCooldown());
    }

    private void ResetAllAttackAnimations()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
    }

    private void ExecuteAttack1()
    {
        animator.SetBool("Attack1", true);
        isAttacking = true;
        StartCoroutine(EndAttackAfterDelay(0.5f, "Attack1"));
    }

    private void ExecuteAttack2()
    {
        animator.SetBool("Attack2", true);
        isAttacking = true;
        StartCoroutine(EndAttackAfterDelay(0.6f, "Attack2"));
    }

    private void ExecuteAttack3()
    {

        animator.SetBool("Attack3", true);
        isAttacking = true;
        StartCoroutine(EndAttackAfterDelay(1.01f, "Attack3"));
    }

    private IEnumerator EndAttackAfterDelay(float delay, string attackParam)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        animator.SetBool(attackParam, false);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator ResetComboAfterDelay()
    {
        yield return new WaitForSeconds(comboWindow);
        currentCombo = 0;
        ResetAllAttackAnimations();
    }


    private IEnumerator ComboCompleteCooldown()
    {

        isComboOnCooldown = true;


        yield return new WaitForSeconds(comboCompleteCooldown);


        currentCombo = 0;
        isComboOnCooldown = false;
        ResetAllAttackAnimations();


    }


    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsComboOnCooldown()
    {
        return isComboOnCooldown;
    }

    public float GetComboCooldownTimeRemaining()
    {
        if (comboCooldownCoroutine != null && isComboOnCooldown)
        {
            return isComboOnCooldown ? -1f : 0f;
        }
        return 0f;
    }

    private void FixedUpdate()
    {
        if (!puedeMoverse) return;

        if (!isDashing && !isWallJumping && !isKnock)
        {
            Move();
        }
    }



    private void Move()
    {
        rb2D.velocity = new Vector2(horizontal * movSpeed, rb2D.velocity.y);
        float velocidadHorizontal = Mathf.Abs(rb2D.velocity.x);

        if (velocidadHorizontal > 0.1f && checkGroundLineCast())
        {
            Debug.Log("Entrando en animación de correr");
            animator.SetBool("Run", true);
            particulas.Play();
        }
        else
        {
            animator.SetBool("Run", false);
        }


    }

    private void Jump()
    {
        if (checkGroundLineCast())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Marcar cuando el jugador suelta la tecla
        if (Input.GetButtonUp("Jump"))
        {
            jumpInputReleased = true;
        }

        // Ignorar si la tecla no ha sido soltada todavía
        if (!jumpInputReleased) return;

        if (Input.GetButtonDown("Jump"))
        {
            if (coyoteTimeCounter > 0f)
            {
                canDoubleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                animator.SetBool("Jump", true);
                soundController?.PlayJumpSound();
            }
            else if (canDoubleJump)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                canDoubleJump = false;
                animator.SetBool("Jump", true);
                coyoteTimeCounter = 0f;
                soundController?.PlayDoubleJumpSound();
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
                isWallJumping = true;
                sr.flipX = true;
                animator.SetBool("WallSlide", false);
                soundController?.PlayJumpSound();
                StartCoroutine(DisableWallJumping());

            }
            else if (checkLeftWallLineCast() && !isWallJumping)
            {
                rb2D.velocity = new Vector2(jumpWallx, jumpWally);
                isWallJumping = true;
                sr.flipX = false;
                animator.SetBool("WallSlide", false);
                soundController?.PlayDoubleJumpSound();
                StartCoroutine(DisableWallJumping());
            }
        }

        if (!checkGroundLineCast() && (checkRightWallLineCast() || checkLeftWallLineCast()))
        {
            animator.SetBool("WallSlide", true);
        }
        else
        {
            animator.SetBool("WallSlide", false);
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
        if (collision.gameObject.CompareTag("tpTuto"))
        {
            transform.position = new Vector2(-2.67f, -4.05f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pinchos"))
        {
            vi.vidasPlayer = 0;
        }
        if (collision.gameObject.CompareTag("keyDoor") && kd.hasKey)
        {
            Destroy(collision.gameObject);

            if (key.activeInHierarchy)
            {
                key.SetActive(false);
            }

        }
    }

    private IEnumerator Knockback(float duration, float powerX, float powerY)
    {
        float timer = 0;
        isKnock = true;
        animator.SetBool("Hurt", true);

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
        animator.SetBool("Hurt", false);
    }

    public bool checkGroundLineCast()
    {
        RaycastHit2D[] hit1 = Physics2D.LinecastAll(transform.position + Vector3.up * 0.5f + Vector3.right * 0.35f, transform.position + Vector3.right * 0.35f + Vector3.down * 0.15f);
        RaycastHit2D[] hit2 = Physics2D.LinecastAll(transform.position + Vector3.up * 0.5f + Vector3.left * 0.35f, transform.position + Vector3.left * 0.35f + Vector3.down * 0.15f);
        RaycastHit2D[] hit3 = Physics2D.LinecastAll(transform.position + Vector3.up * 0.5f, transform.position + Vector3.down * 0.15f);

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
        RaycastHit2D[] hitsTop = Physics2D.LinecastAll(transform.position + Vector3.up * 1.5f, transform.position + Vector3.up * 1.5f + Vector3.right * 0.75f);
        RaycastHit2D[] hitsBottom = Physics2D.LinecastAll(transform.position + Vector3.up * 0.60f, transform.position + Vector3.up * 0.60f + Vector3.right * 0.75f);

        foreach (RaycastHit2D hit in hitsTop)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }

        foreach (RaycastHit2D hit in hitsBottom)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }

        return false;
    }

    private bool checkLeftWallLineCast()
    {
        RaycastHit2D[] hitsTop = Physics2D.LinecastAll(transform.position + Vector3.up * 1.5f, transform.position + Vector3.up * 1.5f + Vector3.left * 0.75f);
        RaycastHit2D[] hitsBottom = Physics2D.LinecastAll(transform.position + Vector3.up * 0.60f, transform.position + Vector3.up * 0.60f + Vector3.left * 0.75f);

        foreach (RaycastHit2D hit in hitsTop)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }

        foreach (RaycastHit2D hit in hitsBottom)
        {
            if (hit.collider.CompareTag("Wall"))
            {
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
        Gizmos.DrawLine(transform.position + Vector3.up * 0.5f + Vector3.right * 0.35f, transform.position + Vector3.right * 0.35f + Vector3.down * 0.15f);
        Gizmos.DrawLine(transform.position + Vector3.up * 0.5f + Vector3.left * 0.35f, transform.position + Vector3.left * 0.35f + Vector3.down * 0.15f);
        Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.down * 0.15f);
        Gizmos.DrawLine(transform.position + Vector3.up * 1.5f, transform.position + Vector3.up * 1.5f + Vector3.right * 0.75f);
        Gizmos.DrawLine(transform.position + Vector3.up * 1.5f, transform.position + Vector3.up * 1.5f + Vector3.left * 0.75f);
        Gizmos.DrawLine(transform.position + Vector3.up * 0.60f, transform.position + Vector3.up * 0.60f + Vector3.right * 0.75f);
        Gizmos.DrawLine(transform.position + Vector3.up * 0.60f, transform.position + Vector3.up * 0.60f + Vector3.left * 0.75f);
    }
}