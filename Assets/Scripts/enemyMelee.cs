using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class enemyMelee : MonoBehaviour
{
   
    public bool isRight = false;
    public bool isLeft = false;
    private bool isPinchos = false;
    private bool isJaula = false;
    public float speed;
    public Transform transformPlayer;
    public bool playerInRange = false;
    public GameObject colision1;
    public GameObject colision2;
    public int vidasEnemigo;
    public SpriteRenderer sr;
    private Animator animator;
    private RangeAttack ra;
    public Rigidbody2D rb;
    private BoxCollider2D bx;
    private float cdHitAnimation = 1.2f;
    private Vidas vd;
    public AudioClip attackSound;
    public AudioClip hitSound;

    private AudioSource audioSource;




    void Start()
    {
        if (speed != 0)
        {
            isRight = true;
        }
      
       sr = GetComponent<SpriteRenderer>();
       animator = GetComponent<Animator>();
       rb = GetComponent<Rigidbody2D>();
       bx = GetComponent<BoxCollider2D>();
       vd = FindObjectOfType<Vidas>();
        audioSource = GetComponent<AudioSource>();


    }

    void Update()
    {
        if (playerInRange)
        {
            Vector2 position = new Vector2(transformPlayer.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);

            speed = 3;

            if (transformPlayer.position.x < transform.position.x)
            {
                sr.flipX = true;

            }
            else
            {
                sr.flipX = false;


            }

            if (vd != null && vd.canHit)
            {
                if (!animator.GetBool("Attack"))
                {
                    animator.SetBool("Attack", true);

                    if (attackSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(attackSound);
                    }
                }
            }

            else
            {
                animator.SetBool("Attack", false);
            }



        }

       else
       {
            
            if (isRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                sr.flipX = false;
               
            }

            else if (isLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                sr.flipX = true;
               
            }

            animator.SetBool("Attack", false);
       }

        if (sr.flipX)
        {
            bx.offset = new Vector2(0.04f, bx.offset.y);
        }

        if (!sr.flipX)
        {
            bx.offset = new Vector2(-0.04f, bx.offset.y);
        }

        if (isPinchos)
        {
            speed = 0;
            vidasEnemigo = 0;
            enemyDead();
            Destroy(gameObject, 1.4f);
            isPinchos = false;
        }
        if (isJaula)
        {
            speed = 0;
            vidasEnemigo = 0;
            enemyDead();
            Destroy(gameObject, 1.4f);
            isJaula = false;
        }

        if (speed == 0)
        {
            animator.SetBool("Idle", true);

        }

        if (vidasEnemigo <= 0)
        {
            speed = 0;
            bx.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    public void enemyDead()
    {
        animator.SetBool("Hit", false);  
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Dead", true);

    }
    public void ReceiveHit()
    {
        sr.color = Color.red;

        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        StartCoroutine(ResetHitAnimation());
    }


    private IEnumerator ResetHitAnimation()
    {
        yield return new WaitForSeconds(cdHitAnimation);
        sr.color = Color.white;
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

        if (collision.gameObject.CompareTag("Jaula"))
        {
            isJaula = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pinchos"))
        {
            isPinchos = true;

        }
     
    }
}
