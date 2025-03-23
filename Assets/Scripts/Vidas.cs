using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vidas : MonoBehaviour
{
    public int vidasPlayer;
    public float vidaCD;
    public bool canHit = true;
    public GameObject[] vidas;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private IEnumerator hit()
    {
        canHit = false;
        yield return new WaitForSeconds(vidaCD);
        canHit = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canHit)
        {
            vidasPlayer--;
            StartCoroutine(hit());
            desactivarVida(vidasPlayer);
        }
    }
    public void desactivarVida(int indice)
    {
        vidas[indice].SetActive(false);

    }
   
}
