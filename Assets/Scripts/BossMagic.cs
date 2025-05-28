using System.Collections;
using UnityEngine;

public class BossMagic : MonoBehaviour
{
    public float warningDuration = 1f;
    public float activeDuration = 2f;
    public Color warningColor = new Color(1f, 0.5f, 0.5f, 0.5f);
    public Color activeColor = new Color(1f, 0f, 0f, 1f);

    private Vidas vd;
    private bool isActive = false;
    private bool hasDamaged = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D magicCollider;

    void Start()
    {
        vd = FindObjectOfType<Vidas>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        magicCollider = GetComponent<Collider2D>();


        if (spriteRenderer != null)
        {
            spriteRenderer.color = warningColor;
        }

        if (magicCollider != null)
        {
            magicCollider.enabled = false;
        }

        StartCoroutine(SpellSequence());
    }

    private IEnumerator SpellSequence()
    {

        yield return new WaitForSeconds(warningDuration);


        isActive = true;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = activeColor;
        }
        if (magicCollider != null)
        {
            magicCollider.enabled = true;
        }


        yield return new WaitForSeconds(activeDuration);


        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && !hasDamaged && collision.CompareTag("Player"))
        {
            if (vd != null)
            {
                hasDamaged = true;
            }
        }
    }


    private void Update()
    {

    }
}
