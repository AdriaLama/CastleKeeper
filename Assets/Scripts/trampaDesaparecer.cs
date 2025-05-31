using System.Collections;
using UnityEngine;

public class trampaDesaparecer : MonoBehaviour
{
    public float cooldownDesactivar;
    public float cooldownActivar;
    private Collider2D miCollider;
    private SpriteRenderer miRenderer;
    private Animator animator;


    void Start()
    {
        miCollider = GetComponent<Collider2D>();
        miRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DestroyTime());
            animator.SetBool("Desaparece", true);

           
        }
    }

    private IEnumerator DestroyTime()
    {
      
        yield return new WaitForSeconds(cooldownDesactivar);
        miCollider.enabled = false;
        miRenderer.enabled = false;
        yield return new WaitForSeconds(cooldownActivar);
        miCollider.enabled = true;
        miRenderer.enabled = true;
        animator.SetBool("Desaparece", false);
        
    }
}
