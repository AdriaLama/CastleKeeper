using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class trampaDesaparecer : MonoBehaviour
{
    public float cooldownDesactivar;
    public float cooldownActivar;
    public AudioClip sonidoDesaparecer;

    private bool enProceso;
    private Collider2D miCollider;
    private SpriteRenderer miRenderer;
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        miCollider = GetComponent<Collider2D>();
        miRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !enProceso)
        {
            StartCoroutine(DestroyTime());
            animator.SetBool("Desaparece", true);

            if (sonidoDesaparecer != null)
                audioSource.PlayOneShot(sonidoDesaparecer);
        }
    }

    private IEnumerator DestroyTime()
    {
        enProceso = true;
        yield return new WaitForSeconds(cooldownDesactivar);
        miCollider.enabled = false;
        miRenderer.enabled = false;
        yield return new WaitForSeconds(cooldownActivar);
        miCollider.enabled = true;
        miRenderer.enabled = true;
        animator.SetBool("Desaparece", false);
        enProceso = false;
    }
}
