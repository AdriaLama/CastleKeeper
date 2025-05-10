using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSala1 : MonoBehaviour
{
    public bool isRight = false;
    public bool isLeft = false;
    public float speed;

    public Transform transformPlayer;
    public bool playerInRange = false;
    public float detectionRange = 8f;
    public int vidasEnemigo;
    public SpriteRenderer sr;
    private Animator animator;
    public Rigidbody2D rb;
    private BoxCollider2D bx;

    private float cdHitAnimation = 1.2f;
    private Vidas vd;
    private bool isDead = false;

    public float attackCooldown;
    public float currentAttackCooldown;
    private bool isAttacking = false;
    private bool useSpellAttack = false;
    public float castDuration;
    public float spellDuration;

    private string currentState;
    private const string IDLE_ANIMATION = "IdleBoss";
    private const string WALK_ANIMATION = "WalkBoss";
    private const string ATTACK_ANIMATION = "AttackBoss";
    private const string HURT_ANIMATION = "HurtBoss";
    private const string DEATH_ANIMATION = "DeathBoss";
    private const string CAST_ANIMATION = "CastBoss";
    private const string SPELL_ANIMATION = "SpellBoss";

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bx = GetComponent<BoxCollider2D>();
        vd = FindObjectOfType<Vidas>();

        if (transformPlayer == null)
        {

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                transformPlayer = player.transform;
            }
        }

        if (speed != 0)
        {
            isRight = true;
        }

        ChangeAnimationState(IDLE_ANIMATION);
    }

    void Update()
    {



        if (isDead || vidasEnemigo <= 0)
        {
            if (currentState != DEATH_ANIMATION)
            {
                ChangeAnimationState(DEATH_ANIMATION);
                enemyDead();
            }
            return;
        }

        if (currentState == HURT_ANIMATION)
        {
            return;
        }

        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        }


        if (transformPlayer != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, transformPlayer.position);
            playerInRange = distanceToPlayer <= detectionRange;
        }

        if (playerInRange)
        {
            if (!isAttacking)
            {
                Vector2 position = new Vector2(transformPlayer.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);


                if (transformPlayer.position.x < transform.position.x)
                {
                    sr.flipX = false;

                }
                else
                {
                    sr.flipX = true;

                }


                if (currentState != WALK_ANIMATION)
                {
                    ChangeAnimationState(WALK_ANIMATION);
                }

                float distanceToPlayer = Vector2.Distance(transform.position, transformPlayer.position);
                if (distanceToPlayer < 2.0f && currentAttackCooldown <= 0)
                {
                    isAttacking = true;

                    if (useSpellAttack)
                    {
                        StartCoroutine(CastSpell());
                    }
                    else
                    {
                        StartCoroutine(PerformAttack());
                    }

                    useSpellAttack = !useSpellAttack;

                    currentAttackCooldown = attackCooldown;
                }
            }
        }
        else
        {
            if (speed > 0 && !isAttacking)
            {
                if (currentState != WALK_ANIMATION)
                {
                    ChangeAnimationState(WALK_ANIMATION);
                }

                if (isRight)
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                    sr.flipX = false;
                }
                else if (isLeft)
                {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    sr.flipX = true;
                }


                if (sr.flipX)
                {
                    bx.offset = new Vector2(1f, bx.offset.y);
                }



            }


            else if (!isAttacking)
            {
                if (currentState != IDLE_ANIMATION)
                {
                    ChangeAnimationState(IDLE_ANIMATION);
                }
            }
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    private IEnumerator PerformAttack()
    {
        float originalSpeed = speed;
        speed = 0;

        ChangeAnimationState(ATTACK_ANIMATION);

        yield return new WaitForSeconds(1.0f);

        float distanceToPlayer = Vector2.Distance(transform.position, transformPlayer.position);
        if (distanceToPlayer < 2.0f && vd.canHit)
        {
            vd.vidasPlayer--;
        }

        speed = originalSpeed;
        isAttacking = false;
    }

    private IEnumerator CastSpell()
    {
        float originalSpeed = speed;
        speed = 0;

        ChangeAnimationState(CAST_ANIMATION);

        yield return new WaitForSeconds(castDuration);

        ChangeAnimationState(SPELL_ANIMATION);

        yield return new WaitForSeconds(spellDuration);

        float distanceToPlayer = Vector2.Distance(transform.position, transformPlayer.position);
        if (distanceToPlayer < 4.0f && vd.canHit)
        {
            vd.vidasPlayer--;
        }

        speed = originalSpeed;
        isAttacking = false;
    }

    public void enemyDead()
    {
        isDead = true;
        ChangeAnimationState(DEATH_ANIMATION);

        speed = 0;
        bx.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void ReceiveHit()
    {
        if (isDead) return;

        vidasEnemigo--;

        if (vidasEnemigo <= 0)
        {
            enemyDead();
            return;
        }

        ChangeAnimationState(HURT_ANIMATION);
        StartCoroutine(ResetHitAnimation());
    }

    private IEnumerator ResetHitAnimation()
    {
        yield return new WaitForSeconds(cdHitAnimation);

        if (speed == 0)
        {
            ChangeAnimationState(IDLE_ANIMATION);
        }
        else
        {
            ChangeAnimationState(WALK_ANIMATION);
        }
    }

    public void SetPlayerInRange(bool inRange)
    {
        this.playerInRange = inRange;
    }


}