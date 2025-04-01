using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vidas : MonoBehaviour
{
    public int vidasPlayer;
    public float vidaCD;
    public bool canHit = true;
    public bool hasHit;
    public GameObject[] vidas;
    [SerializeField]
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(vidasPlayer <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("MenuPrincipal");
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
            hasHit = true;
            if (hasHit)
            {
                animator.SetBool("Attack", true);
                hasHit = false;
            }
           
        }
        if (collision.gameObject.CompareTag("ObjDaño") && canHit)
        {
            vidasPlayer--;
            StartCoroutine(hit());
            desactivarVida(vidasPlayer);
        }
        if (collision.gameObject.CompareTag("Bullet") && canHit)
        {
            vidasPlayer--;
            StartCoroutine(hit());
            desactivarVida(vidasPlayer);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PinchosPlataforma") && canHit)
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
