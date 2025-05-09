using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform puntoA, puntoB;
    private float velocidad0;
    public float velocidadDefault;

    private Vector3 destino;

    void Start()
    {
        velocidad0 = velocidadDefault;
        destino = puntoB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad0 * Time.deltaTime);

        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            destino = (destino == puntoA.position) ? puntoB.position : puntoA.position;
        }
    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
           
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.activeInHierarchy && collision.gameObject != null)
        {
            collision.transform.SetParent(null);
        }
    }
}