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
    public int vidasEnemigo;
    private SpriteRenderer sr;
    private Animator animator;

    void Start()
    {
       isRight = true;
       sr = GetComponent<SpriteRenderer>();
       animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, transformPlayer.position, speed * Time.deltaTime);

            if (transformPlayer.position.x < transform.position.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }

       else
       {
            
            if (isRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                sr.flipX = false;
            }

            if (isLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                sr.flipX = true;
            }

            animator.SetBool("Attack", false);
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
