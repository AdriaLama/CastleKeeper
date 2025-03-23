using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class RangeAttack : MonoBehaviour
{
    public bool isTrue = false; 
    private GameObject enemy;
    private MovimientoPJ movimientoPj;
    private BoxCollider2D boxCollider;
    public int countAttacks;
    private enemyMelee em;
    public bool canAttack = true;
    public float cdAttack;

    private void Start()
    {

        boxCollider = GetComponent<BoxCollider2D>();
        movimientoPj = GetComponentInParent<MovimientoPJ>();
        em = GetComponent<enemyMelee>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTrue = true;
            enemy = collision.gameObject;
            em = enemy.GetComponent<enemyMelee>();
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
