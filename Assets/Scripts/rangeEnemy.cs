using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeEnemy : MonoBehaviour
{
    private bool isRight = false;
    private bool isLeft = false;
    public float speed;
    public float speedDefault;
    public Transform transformPlayer;
    public bool playerInRange = false;
    public GameObject colision1;
    public GameObject colision2;
    public int vidasEnemigo;
    public Transform controladorDisparo;
    public float distancialinea;
    public float TiempoDeDisparos;
    public float TiempoDeUltimaBala;
    public GameObject balaEnemigo;
    private SpriteRenderer sr;
    private Vector2 direccionDisparo;
    private Animator animator;
    private float cdHitAnimation = 1.2f;
    public AudioClip attackSound;
    public AudioClip hitSound;

    private AudioSource audioSource;

    void Start()
    {
        isRight = true;
        speedDefault = speed;
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (playerInRange)
        {
            animator.SetBool("Attack", true);
            speed = 0;

            direccionDisparo = (transformPlayer.position - transform.position).normalized;

            if (transformPlayer.position.x < transform.position.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            if (Time.time > TiempoDeDisparos + TiempoDeUltimaBala)
            {
                TiempoDeUltimaBala = Time.time;
                Invoke(nameof(Disparar), TiempoDeDisparos);
            }
          
        
        }
        else
        {

            if (isRight)
            {
                speed = speedDefault;
                transform.position += Vector3.right * speed * Time.deltaTime;
                sr.flipX = false;
            }

            if (isLeft)
            {
                speed = speedDefault;
                transform.position += Vector3.left * speed * Time.deltaTime;
                sr.flipX = true;
            }

            animator.SetBool("Attack", false);

        }
    }

    public void enemyDead()
    {
        animator.SetBool("Hit", false);
        animator.SetBool("Attack", false);
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
    public void Disparar()
    {
        GameObject nuevaBala = Instantiate(balaEnemigo, controladorDisparo.position, controladorDisparo.rotation);
        nuevaBala.GetComponent<BulletEnemyRange>().SetDireccion(direccionDisparo);

        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
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
