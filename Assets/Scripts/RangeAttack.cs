using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class RangeAttack : MonoBehaviour
{
    public bool isTrue = false; 
    public bool isTrueRange = false; 
    private GameObject enemy;
    private GameObject enemyRange;
    private MovimientoPJ movimientoPj;
    private BoxCollider2D boxCollider;
    private enemyMelee em;
    private rangeEnemy re;
    public bool canAttack = true;
    public float cdAttack;
    private bool isJaula = false;
    public bool JaulaFall = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        movimientoPj = GetComponentInParent<MovimientoPJ>();
        em = GetComponent<enemyMelee>();
        re = GetComponent<rangeEnemy>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTrue = true;
            enemy = collision.gameObject;
            em = enemy.GetComponent<enemyMelee>();
          
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canAttack)
        {
            movimientoPj.animator.SetBool("Attack", true);
            StartCoroutine(ResetAttackAnimation());

            if (isTrue)
            {
                if(em.vidasEnemigo > 0)
                {
                    em.ReceiveHit();
                }
               
                em.vidasEnemigo--;
                canAttack = false;

                if (em.vidasEnemigo >= 1)
                {
                    if (em.sr.flipX)
                    {
                        em.rb.velocity = new Vector2(5, 3);

                    }
                    if (!(em.sr.flipX))
                    {
                        em.rb.velocity = new Vector2(-5, 3);

                    }
                }
               

                if (em.vidasEnemigo <= 0)
                {
                    em.speed = 0;
                    em.enemyDead();
                    Destroy(enemy.gameObject, 1.40f);
                }

                StartCoroutine(RangeAttackCD());


            }

            if (isTrueRange)
            {
                if (re.vidasEnemigo > 0)
                {
                    re.ReceiveHit();
                }
                re.speed = 0;
                re.vidasEnemigo--;
                canAttack = false;

                if (re.vidasEnemigo <= 0)
                {
                    re.speed = 0;
                    re.enemyDead();
                    Destroy(enemyRange.gameObject, 0.5f);
                }

                StartCoroutine(RangeAttackCD());

            }

            if (isJaula)
            {
                JaulaFall = true;
            }

           
        }
        
        if (movimientoPj.isLeft)
        {
            boxCollider.offset = new Vector2(-1.5f, boxCollider.offset.y);
            
        }
        if (movimientoPj.isRight)
        {
            boxCollider.offset = new Vector2(1.5f, boxCollider.offset.y);

        }
    }

    private IEnumerator RangeAttackCD()
    {
        canAttack = false;
        yield return new WaitForSeconds(cdAttack);
        canAttack = true;
    }
    private IEnumerator ResetAttackAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        movimientoPj.animator.SetBool("Attack", false);
    }





}
