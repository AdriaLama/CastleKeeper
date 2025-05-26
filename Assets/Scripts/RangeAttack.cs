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
    public float[] attackDamage = { 1, 1, 2 }; // Daño por cada ataque del combo
    public float[] attackKnockback = { 3f, 4f, 6f }; // Knockback por cada ataque
    public float[] attackRange = { 1.5f, 1.5f, 2f }; // Rango de cada ataque

    private PlayerSoundController soundController;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        movimientoPj = GetComponentInParent<MovimientoPJ>();
        soundController = GetComponent<PlayerSoundController>();
    }

    private void Update()
    {
        // El input de ataque ahora se maneja en MovimientoPJ
        // Solo necesitamos verificar si el jugador está atacando y aplicar el daño correspondiente

        if (movimientoPj.IsAttacking())
        {
            int currentCombo = movimientoPj.GetCurrentCombo();
            ApplyAttackEffects(currentCombo);
        }

        // Ajustar el collider según la dirección y el ataque actual
        AdjustAttackCollider();
    }

    private void ApplyAttackEffects(int comboStep)
    {
        if (comboStep < 1 || comboStep > 3) return;

        int index = comboStep - 1; // Convert to array index

        // Aplicar efectos a enemigo melee
        if (isTrue && enemy != null && em != null && em.vidasEnemigo > 0)
        {
            ApplyDamageToMeleeEnemy(index);
        }

        // Aplicar efectos a boss
        if (isTrue && boss != null && te != null && te.vidasEnemigo > 0)
        {
            ApplyDamageToBoss(index);
        }

        // Aplicar efectos a enemigo de rango
        if (isTrueRange && enemyRange != null && re != null && re.vidasEnemigo > 0)
        {
            ApplyDamageToRangeEnemy(index);
        }

        // Aplicar efectos a jaula
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

            // Aplicar knockback más fuerte para ataques posteriores del combo
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

        Debug.Log($"Ataque {attackIndex + 1} aplicado a enemigo melee. Daño: {attackDamage[attackIndex]}");
    }

    private void ApplyDamageToBoss(int attackIndex)
    {
        te.ReceiveHit();
        te.vidasEnemigo -= (int)attackDamage[attackIndex];

        if (te.vidasEnemigo <= 0)
        {
            te.enemyDead();
        }

        Debug.Log($"Ataque {attackIndex + 1} aplicado a boss. Daño: {attackDamage[attackIndex]}");
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

        Debug.Log($"Ataque {attackIndex + 1} aplicado a enemigo de rango. Daño: {attackDamage[attackIndex]}");
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
            // Posición por defecto del collider
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

    // Método para debugging - mostrar información del combo actual
    private void OnGUI()
    {
        if (movimientoPj != null && movimientoPj.IsAttacking())
        {
            GUI.Label(new Rect(10, 10, 200, 20), $"Combo Actual: {movimientoPj.GetCurrentCombo()}");
            GUI.Label(new Rect(10, 30, 200, 20), $"Atacando: {movimientoPj.IsAttacking()}");
        }
    }
}