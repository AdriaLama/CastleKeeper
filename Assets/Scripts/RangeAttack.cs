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
    private bool isJaula = false;
    public bool JaulaFall = false;

    [Header("Combat Settings")]
    public float[] attackDamage = { 1, 1, 2 }; 
    public float[] attackKnockback = { 3f, 4f, 6f }; 
    public float[] attackRange = { 1.5f, 1.5f, 2f }; 

    
    private bool[] hasDealtDamage = { false, false, false };
    private int lastComboProcessed = 0;

    private PlayerSoundController soundController;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        movimientoPj = GetComponentInParent<MovimientoPJ>();
        soundController = GetComponent<PlayerSoundController>();
    }

    private void Update()
    {
        if (movimientoPj.IsAttacking())
        {
            int currentCombo = movimientoPj.GetCurrentCombo();

           
            if (currentCombo != lastComboProcessed && currentCombo >= 1 && currentCombo <= 3)
            {
                ApplyAttackEffects(currentCombo);
                hasDealtDamage[currentCombo - 1] = true;
                lastComboProcessed = currentCombo;
            }
        }
        else
        {
            
            if (lastComboProcessed > 0)
            {
                ResetDamageFlags();
                lastComboProcessed = 0;
            }
        }

        
        AdjustAttackCollider();
    }

    private void ResetDamageFlags()
    {
        for (int i = 0; i < hasDealtDamage.Length; i++)
        {
            hasDealtDamage[i] = false;
        }
    }

    private void ApplyAttackEffects(int comboStep)
    {
        if (comboStep < 1 || comboStep > 3) return;

        int index = comboStep - 1; 

        
        if (hasDealtDamage[index]) return;

       
        if (isTrue && enemy != null && em != null && em.vidasEnemigo > 0)
        {
            ApplyDamageToMeleeEnemy(index);
        }

        
        if (isTrue && boss != null && te != null && te.vidasEnemigo > 0)
        {
            ApplyDamageToBoss(index);
        }

        
        if (isTrueRange && enemyRange != null && re != null && re.vidasEnemigo > 0)
        {
            ApplyDamageToRangeEnemy(index);
        }

        
        if (isJaula)
        {
            JaulaFall = true;
        }
    }

    private void ApplyDamageToMeleeEnemy(int attackIndex)
    {
        em.ReceiveHit();
        em.vidasEnemigo -= (int)attackDamage[attackIndex];

        if (em.vidasEnemigo >= 1)
        {
            float knockbackForce = attackKnockback[attackIndex];

            
            if (em.sr.flipX)
                em.rb.velocity = new Vector2(knockbackForce, 3);
            else
                em.rb.velocity = new Vector2(-knockbackForce, 3);
        }

        if (em.vidasEnemigo <= 0)
        {
            em.speed = 0;
            em.enemyDead();
            Destroy(enemy.gameObject, 1.40f);
        }       
    }

    private void ApplyDamageToBoss(int attackIndex)
    {
        te.ReceiveHit();
        te.vidasEnemigo -= (int)attackDamage[attackIndex];

        if (te.vidasEnemigo <= 0)
        {
            te.enemyDead();
        }
       
    }

    private void ApplyDamageToRangeEnemy(int attackIndex)
    {
        re.ReceiveHit();
        re.speed = 0;
        re.vidasEnemigo -= (int)attackDamage[attackIndex];

        if (re.vidasEnemigo <= 0)
        {
            re.enemyDead();
            Destroy(enemyRange.gameObject, 0.5f);
        }       
    }

    private void AdjustAttackCollider()
    {
        if (movimientoPj.IsAttacking())
        {
            int currentCombo = movimientoPj.GetCurrentCombo();
            if (currentCombo >= 1 && currentCombo <= 3)
            {
                float range = attackRange[currentCombo - 1];

                if (movimientoPj.isLeft)
                {
                    boxCollider.offset = new Vector2(-range, boxCollider.offset.y);
                }
                if (movimientoPj.isRight)
                {
                    boxCollider.offset = new Vector2(range, boxCollider.offset.y);
                }
            }
        }
        else
        {
            
            if (movimientoPj.isLeft)
            {
                boxCollider.offset = new Vector2(-0.50f, boxCollider.offset.y);
            }
            if (movimientoPj.isRight)
            {
                boxCollider.offset = new Vector2(0.50f, boxCollider.offset.y);
            }
        }
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