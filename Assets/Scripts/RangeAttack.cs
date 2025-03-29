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
    public int countAttacks;
    private enemyMelee em;
    private rangeEnemy re;
    public bool canAttack = true;
    public float cdAttack;

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
    }
    private void Update()
    {
        if (isTrue && Input.GetKeyDown(KeyCode.Q))
        {
            em.vidasEnemigo--;
            
            if(em.vidasEnemigo <= 0)
            {
                Destroy(enemy.gameObject);
            }
        }

        if (isTrueRange && Input.GetKeyDown(KeyCode.Q))
        {
            re.vidasEnemigo--;

            if (re.vidasEnemigo <= 0)
            {
                Destroy(enemyRange.gameObject);
            }

        }
        if (movimientoPj.isLeft)
        {
            boxCollider.offset = new Vector2(-2.11f, boxCollider.offset.y);
            
        }
        if (movimientoPj.isRight)
        {
            boxCollider.offset = new Vector2(2.11f, boxCollider.offset.y);

        }
    }

}
