using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform puntoA, puntoB; // Puntos de movimiento
    public float velocidad = 2f; // Velocidad de la plataforma

    private Vector3 destino;

    void Start()
    {
        destino = puntoB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            destino = (destino == puntoA.position) ? puntoB.position : puntoA.position;
        }
    }

    // Cuando el jugador toca la plataforma, se vuelve hijo de ella
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    // Cuando el jugador sale de la plataforma, se suelta
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}