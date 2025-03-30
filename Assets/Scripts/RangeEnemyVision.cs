using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyVision : MonoBehaviour
{
    public rangeEnemy re_enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            re_enemy.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            re_enemy.SetPlayerInRange(false);
        }
    }
}
