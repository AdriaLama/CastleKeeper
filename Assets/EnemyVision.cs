using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    [SerializeField]
    private enemyMelee sc_enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Entro");
        if (collision.CompareTag("Player"))
        {
            sc_enemy.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sc_enemy.SetPlayerInRange(false);
        }
    }

}
