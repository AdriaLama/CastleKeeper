using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyMelee : MonoBehaviour
{
    public float counter;
    public float waitingTime;
    private bool isRight;
    public float speed;
    public float radiumSearch;
    public LayerMask capaPlayer;
    public Transform transformPlayer;
    public bool playerInRange = false;
    private Vector3 initialPosition;


    public enum EstadosMovimiento {ESPERANDO, SIGUIENDO, REGRESANDO};
    public EstadosMovimiento estadoActual;

    void Start()
    {
        counter = waitingTime;
        initialPosition = transform.position;
    }



    void Update()
    {
        if (playerInRange)
        {
            Vector3 direction = (transformPlayer.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

        }

       else
       {
            initialPosition = transform.position;

            if (isRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;

            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;

            }
            counter -= Time.deltaTime;

            if (counter <= 0)
            {
                counter = waitingTime;
                isRight = !isRight;

            }

       }


    }


    public void SetPlayerInRange(bool inRange)
    {
        this.playerInRange = inRange;
    }
}
