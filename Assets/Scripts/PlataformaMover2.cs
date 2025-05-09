using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlatformMover2 : MonoBehaviour
{
    public Transform puntoB;
    private float velocidad0;
    public float velocidadDefault;

    private Vector3 destino;

    void Start()
    {
        velocidad0 = 0;
        destino = puntoB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad0 * Time.deltaTime);

        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            velocidadDefault = velocidad0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            velocidad0 = velocidadDefault;

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