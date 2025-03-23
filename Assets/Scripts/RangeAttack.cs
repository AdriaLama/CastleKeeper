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

    private void Start()
    {

        boxCollider = GetComponent<BoxCollider2D>();
        movimientoPj = GetComponentInParent<MovimientoPJ>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTrue = true;
            enemy = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTrue = false;
            enemy = null;
        }
    }
    private void Update()
    {
        if (isTrue && Input.GetKeyDown(KeyCode.Q))
        {
            countAttacks++;
            if( countAttacks >= 2)
            {
                Destroy(enemy.gameObject);
                countAttacks = 0;
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
