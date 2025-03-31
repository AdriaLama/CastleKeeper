using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampaTecho : MonoBehaviour
{
    public float cooldownTrampa;
    public bool playerInRange = false;
    public float speed;
    public float speedUp;
    private Vector3 initialPosition;
    private bool isInCooldown = false;
    private bool isFalling = false;
    private bool isReturning = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Si el jugador está en rango y no estamos en cooldown, la trampa cae
        if (playerInRange && !isInCooldown && !isReturning && !isFalling)
        {
            isFalling = true;
        }

        // Movimiento de caída
        if (isFalling)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        // Movimiento de retorno
        if (isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speedUp * Time.deltaTime);

            // Si ya regresó a su posición inicial
            if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
            {
                isReturning = false;
                isInCooldown = false; // Fin del cooldown cuando regresa a posición
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }

        if (collision.gameObject.CompareTag("Floor") && isFalling)
        {
            isFalling = false;
            StartCoroutine(InitiateCooldown());
        }
    }

    public void SetPlayerInRange(bool inRange)
    {
        this.playerInRange = inRange;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private IEnumerator InitiateCooldown()
    {
        isInCooldown = true;

        // Esperar antes de comenzar a subir
        yield return new WaitForSeconds(cooldownTrampa);

        // Comenzar a subir de regreso
        isReturning = true;
    }
}