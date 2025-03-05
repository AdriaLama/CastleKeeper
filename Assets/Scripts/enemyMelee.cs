using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyMelee : MonoBehaviour
{

    private bool isRight = false;
    private bool isLeft = false;
    public float speed;
    public Transform transformPlayer;
    public bool playerInRange = false;
    public GameObject colision1;
    public GameObject colision2;
    

    void Start()
    {
        
       isRight = true; 
       
    }

    void Update()
    {


        if (playerInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, transformPlayer.position, speed * Time.deltaTime);

        }

       else
       {
            
            if (isRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;

            }

            if (isLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;

            }
            
       }


    }

    public void SetPlayerInRange(bool inRange)
    {
        this.playerInRange = inRange;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Colision1"))
        {
            isRight = true;
            isLeft = false;
        }
        if (collision.gameObject.CompareTag("Colision2"))
        {
            isLeft = true;
            isRight = false;
        }
    }

}
