using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public bool isTrue = false;
    public bool isTrueRange = false;
    private GameObject enemy;
    private GameObject boss;
    private GameObject enemyRange;
    private MovimientoPJ movimientoPj;
    private BoxCollider2D boxCollider;
    private enemyMelee em;
    private rangeEnemy re;
    private BossSala1 te;
    public bool canAttack = true;
    public float cdAttack;
    private bool isJaula = false;
    public bool JaulaFall = false;

    // 🎧 Referencia al controlador de sonido
    private PlayerSoundController soundController;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        movimientoPj = GetComponentInParent<MovimientoPJ>();
        soundController = GetComponent<PlayerSoundController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canAttack)
        {
            movimientoPj.animator.SetBool("Attack", true);
            StartCoroutine(ResetAttackAnimation());

            // 🎧 Reproducir sonido de ataque
            soundController?.PlayAttackSound();

            // Atacar a enemigo cuerpo a cuerpo
            if (isTrue && enemy != null && em != null)
            {
                if (em.vidasEnemigo > 0)
                {
                    em.ReceiveHit();
                    em.vidasEnemigo--;
                    canAttack = false;

                    if (em.vidasEnemigo >= 1)
                    {
                        if (em.sr.flipX)
                            em.rb.velocity = new Vector2(5, 3);
                        else
                            em.rb.velocity = new Vector2(-5, 3);
                    }

                    if (em.vidasEnemigo <= 0)
                    {
                        em.speed = 0;
                        em.enemyDead();
                        Destroy(enemy.gameObject, 1.40f);
                    }
                }

                StartCoroutine(RangeAttackCD());
            }

            // Atacar al jefe
            if (isTrue && boss != null && te != null)
            {
                if (te.vidasEnemigo > 0)
                {
                    te.ReceiveHit();
                    te.vidasEnemigo--;
                    canAttack = false;

                    if (te.vidasEnemigo <= 0)
                    {
                        te.enemyDead();
                    }
                }

                StartCoroutine(RangeAttackCD());
            }

            // Atacar a enemigo a distancia
            if (isTrueRange && enemyRange != null && re != null)
            {
                if (re.vidasEnemigo > 0)
                {
                    re.ReceiveHit();
                    re.speed = 0;
                    re.vidasEnemigo--;
                    canAttack = false;

                    if (re.vidasEnemigo <= 0)
                    {
                        re.enemyDead();
                        Destroy(enemyRange.gameObject, 0.5f);
                    }
                }

                StartCoroutine(RangeAttackCD());
            }

            // Activar caída de jaula
            if (isJaula)
            {
                JaulaFall = true;
            }
        }

        // Posición del box collider para ataque
        if (movimientoPj.isLeft)
        {
            boxCollider.offset = new Vector2(-1.5f, boxCollider.offset.y);
        }
        if (movimientoPj.isRight)
        {
            boxCollider.offset = new Vector2(1.5f, boxCollider.offset.y);
        }
    }

    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        movimientoPj.animator.SetBool("Attack", false);
    }

    private IEnumerator RangeAttackCD()
    {
        canAttack = false;
        yield return new WaitForSeconds(cdAttack);
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTrue = true;
            enemy = collision.gameObject;
            em = enemy.GetComponent<enemyMelee>();
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            isTrue = true;
            boss = collision.gameObject;
            te = boss.GetComponent<BossSala1>();
        }

        if (collision.gameObject.CompareTag("EnemyRange"))
        {
            isTrueRange = true;
            enemyRange = collision.gameObject;
            re = enemyRange.GetComponent<rangeEnemy>();
        }

        if (collision.gameObject.CompareTag("Jaula"))
        {
            isJaula = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTrue = false;
            enemy = null;
            em = null;
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            isTrue = false;
            boss = null;
            te = null;
        }

        if (collision.gameObject.CompareTag("EnemyRange"))
        {
            isTrueRange = false;
            enemyRange = null;
            re = null;
        }

        if (collision.gameObject.CompareTag("Jaula"))
        {
            isJaula = false;
        }
    }
}
