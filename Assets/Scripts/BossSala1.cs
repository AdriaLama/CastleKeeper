using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BossSala1 : MonoBehaviour
{
    public bool isRight = false;
    public bool isLeft = false;
    public float speed;

    public Transform transformPlayer;
    public bool playerInRange = false;
    public float detectionRange = 100f;
    public int vidasEnemigo;
    public SpriteRenderer sr;
    private Animator animator;
    public Rigidbody2D rb;
    private BoxCollider2D bx;
    public Transform controladorAtaque;
    public float radioAtaque;
    public GameObject magic;
    public GameObject doorFinish;
    private bool doorSpawned = false;

    private float cdHitAnimation = 1.2f;
    private Vidas vd;
    private bool isDead = false;

    public float attackCooldown;
    public float currentAttackCooldown;
    private bool isAttacking = false;
    private bool useSpellAttack = false;
    public float castDuration;
    public float spellDuration;

    
    public float teleportCooldown = 4f; 
    private float currentTeleportCooldown;
    public float teleportChance = 0.4f; 
    private bool isTeleporting = false;
    public float teleportRange = 10f; 
    public float vanishDuration = 1f; 
    public float respawnDuration = 1f; 
    private bool hasAttackedOnce = false; 

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

        currentTeleportCooldown = teleportCooldown; 
        hasAttackedOnce = false;
        ResetAllAnimations();
        animator.SetBool("IdleBoss", true);
    }

    void Update()
    {
        if (isDead || vidasEnemigo <= 0)
        {
            if (!animator.GetBool("DeathBoss"))
            {
                ResetAllAnimations();
                animator.SetBool("DeathBoss", true);
                enemyDead();
            }
            return;
        }

        if (animator.GetBool("HurtBoss") || isTeleporting)
        {
            return;
        }

        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        }

        if (currentTeleportCooldown > 0)
        {
            currentTeleportCooldown -= Time.deltaTime;
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

                if (!animator.GetBool("WalkBoss"))
                {
                    ResetAllAnimations();
                    animator.SetBool("WalkBoss", true);
                }

                float distanceToPlayer = Vector2.Distance(transform.position, transformPlayer.position);
                if (distanceToPlayer < 5.0f && currentAttackCooldown <= 0)
                {
                    
                    if (hasAttackedOnce && currentTeleportCooldown <= 0 && Random.Range(0f, 1f) < teleportChance)
                    {
                        StartCoroutine(PerformTeleport());
                    }
                    else
                    {
                        isAttacking = true;
                        hasAttackedOnce = true; 

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
        }
        else
        {
            if (speed > 0 && !isAttacking)
            {
                if (!animator.GetBool("WalkBoss"))
                {
                    ResetAllAnimations();
                    animator.SetBool("WalkBoss", true);
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
                if (!animator.GetBool("IdleBoss"))
                {
                    ResetAllAnimations();
                    animator.SetBool("IdleBoss", true);
                }
            }
        }
    }

    private void ResetAllAnimations()
    {
        animator.SetBool("IdleBoss", false);
        animator.SetBool("WalkBoss", false);
        animator.SetBool("AttackBoss", false);
        animator.SetBool("HurtBoss", false);
        animator.SetBool("DeathBoss", false);
        animator.SetBool("CastBoss", false);
        animator.SetBool("SpellBoss", false);
        animator.SetBool("VanishBoss", false);
        animator.SetBool("RespawnBoss", false);
    }

    private IEnumerator PerformTeleport()
    {
        isTeleporting = true;
        float originalSpeed = speed;
        speed = 0;        
        ResetAllAnimations();
        animator.SetBool("VanishBoss", true);

        yield return new WaitForSeconds(vanishDuration);
        
        Vector3 newPosition = CalculateTeleportPosition();
        transform.position = newPosition;
       
        if (transformPlayer != null)
        {
            if (transformPlayer.position.x < transform.position.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
      
        ResetAllAnimations();
        animator.SetBool("RespawnBoss", true);

        yield return new WaitForSeconds(respawnDuration);

        ResetAllAnimations();
        if (speed > 0)
        {
            animator.SetBool("WalkBoss", true);
        }
        else
        {
            animator.SetBool("IdleBoss", true);
        }

        speed = originalSpeed;
        isTeleporting = false;
        currentTeleportCooldown = teleportCooldown;
    }

    private Vector3 CalculateTeleportPosition()
    {
        Vector3 playerPos = transformPlayer.position;
        Vector3 currentPos = transform.position;
       
        for (int i = 0; i < 10; i++)
        {
            
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(3f, teleportRange);

            Vector3 offset = new Vector3(
            Mathf.Cos(angle) * distance, 0f, 0f);
            Vector3 newPos = playerPos + offset;
            newPos.y = currentPos.y; 
            
            if (Vector2.Distance(new Vector2(newPos.x, newPos.y), new Vector2(playerPos.x, playerPos.y)) > 2f)
            {
                return newPos;
            }
        }
        
        Vector3 fallbackPos = playerPos;
        fallbackPos.x += (playerPos.x > currentPos.x) ? -5f : 5f;
        fallbackPos.y = currentPos.y;

        return fallbackPos;
    }

    private IEnumerator PerformAttack()
    {
        float originalSpeed = speed;
        speed = 0;

        ResetAllAnimations();
        animator.SetBool("AttackBoss", true);

        yield return new WaitForSeconds(1.0f);

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D collision in objetos)
        {
            if (collision.CompareTag("Player"))
            {
                vd.vidasPlayer--;
                vd.StartCoroutine(vd.hit());
                vd.desactivarVida(vd.vidasPlayer);
                vd.playerHit = true;
            }
        }

        ResetAllAnimations();
        if (speed > 0)
        {
            animator.SetBool("WalkBoss", true);
        }
        else
        {
            animator.SetBool("IdleBoss", true);
        }

        speed = originalSpeed;
        isAttacking = false;
    }

    private IEnumerator CastSpell()
    {
        float originalSpeed = speed;
        speed = 0;

        ResetAllAnimations();
        animator.SetBool("CastBoss", true);

        yield return new WaitForSeconds(castDuration);

        Vector3 targetPosition = transformPlayer.position;
        GameObject ability = Instantiate(magic, targetPosition, Quaternion.identity);

        ResetAllAnimations();

        if (speed > 0)
        {
            animator.SetBool("WalkBoss", true);
        }
        else
        {
            animator.SetBool("IdleBoss", true);
        }

        speed = originalSpeed;
        isAttacking = false;
    }

    public void enemyDead()
    {
        isDead = true;
        ResetAllAnimations();
        animator.SetBool("DeathBoss", true);
        StartCoroutine(SpawnDoorAfterDelay());
        Destroy(gameObject, 3.1f);
        speed = 0;
        bx.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void ReceiveHit()
    {
        if (isDead || isTeleporting) return; 

        if (vidasEnemigo <= 0)
        {
            enemyDead();
            return;
        }

        ResetAllAnimations();
        animator.SetBool("HurtBoss", true);
        StartCoroutine(ResetHitAnimation());
    }

    private IEnumerator SpawnDoorAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (doorFinish != null && !doorSpawned)
        {
            doorFinish.SetActive(true);
            doorSpawned = true;
        }
    }

    private IEnumerator ResetHitAnimation()
    {
        yield return new WaitForSeconds(cdHitAnimation);

        ResetAllAnimations();
        if (speed == 0)
        {
            animator.SetBool("IdleBoss", true);
        }
        else
        {
            animator.SetBool("WalkBoss", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);

        
        Gizmos.color = Color.blue;
        if (transformPlayer != null)
        {
            Gizmos.DrawWireSphere(transformPlayer.position, teleportRange);
        }
    }
}