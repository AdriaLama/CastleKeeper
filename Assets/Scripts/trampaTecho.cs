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

        if (playerInRange && !isInCooldown && !isReturning && !isFalling)
        {
            isFalling = true;
        }


        if (isFalling)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }


        if (isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speedUp * Time.deltaTime);

            if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
            {
                isReturning = false;
                StartCoroutine(FinishCooldown());
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
            isInCooldown = true;
            isReturning = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


    public void SetPlayerInRange(bool inRange)
    {
        this.playerInRange = inRange;
    }

    private IEnumerator FinishCooldown()
    {

        yield return new WaitForSeconds(cooldownTrampa);

        isInCooldown = false;
    }
}