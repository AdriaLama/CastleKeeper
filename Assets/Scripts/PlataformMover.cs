using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform puntoA, puntoB;
    private float velocidad0;
    public float velocidadDefault;
    public float moveCD = 2.5f;
    public bool canMove = true;
    public bool waiting = false;

    private Vector3 destino;

    void Start()
    {
        velocidad0 = velocidadDefault;
        destino = puntoB.position;
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad0 * Time.deltaTime);
        }
       

        if (Vector3.Distance(transform.position, destino) < 0.1f && !waiting)
        {

            StartCoroutine(plataformaCD());

           
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

    private IEnumerator plataformaCD()
    {
        canMove = false;
        waiting = true;
        yield return new WaitForSeconds(moveCD);

        destino = (destino == puntoB.position) ? puntoA.position : puntoB.position;

        canMove = true;
        waiting = false;

    }
}