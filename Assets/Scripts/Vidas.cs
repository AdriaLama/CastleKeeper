using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vidas : MonoBehaviour
{
    public int maxVidas;
    public int vidasPlayer;
    public float vidaCD;
    public bool canHit = true;
    public bool hasHit;
    public bool playerHit;
    public GameObject[] vidas;
    private Animator animator;
    public GameObject gameOverUI;
    private bool isDead = false;

    void Start()
    {
        animator = FindObjectOfType<Animator>();
        vidasPlayer = maxVidas;
    }

    void Update()
    {
        if(vidasPlayer <= 0)
        {
            gameOverUI.SetActive(true); 
            Time.timeScale = 0f; 
            Destroy(this.gameObject);
            
        }
        
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
            playerHit = true;

        }
        if (collision.gameObject.CompareTag("ObjDaño") && canHit)
        {
            vidasPlayer--;
            StartCoroutine(hit());
            desactivarVida(vidasPlayer);
            playerHit = true;

        }
        if (collision.gameObject.CompareTag("Bullet") && canHit)
        {
            vidasPlayer--;
            StartCoroutine(hit());
            desactivarVida(vidasPlayer);
            Destroy(collision.gameObject);
            playerHit = true;

        }
        if (collision.gameObject.CompareTag("PinchosPlataforma") && canHit)
        {
            vidasPlayer--;
            StartCoroutine(hit());
            desactivarVida(vidasPlayer);
            playerHit = true;

        }
        if (collision.gameObject.CompareTag("Pendulum") && canHit)
        {
            vidasPlayer--;
            StartCoroutine(hit());
            desactivarVida(vidasPlayer);
            playerHit = true;

        }
        if (collision.gameObject.CompareTag("Boss") && canHit)
        {
            vidasPlayer--;
            StartCoroutine(hit());
            desactivarVida(vidasPlayer);
            playerHit = true;

        }
    }

    public void desactivarVida(int indice)
    {
        vidas[indice].SetActive(false);

    }
   
}
