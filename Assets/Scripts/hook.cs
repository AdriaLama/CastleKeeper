using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
    LineRenderer line;
    private Rigidbody2D rb2D;
    private PickItems pi;
    public LayerMask grapplableMask;
    public float maxDistance;
    public float grappleSpeed;
    public float grappleShootSpeed;
    public bool isGrappling = false;
    public bool retracting = false;
    public float hookCD = 1.5f; // Ajusta el cooldown a un valor razonable
    private bool canHook = true;
    Vector2 target;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        pi = GetComponent<PickItems>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isGrappling && canHook && pi.hasHook)
        {
            StartCoroutine(StartGrapple());
        }

        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
            transform.position = grapplePos;
            line.enabled = false;

            if (Vector2.Distance(transform.position, target) < 1f)
            {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
                rb2D.constraints = RigidbodyConstraints2D.None;
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private IEnumerator StartGrapple()
    {
        canHook = false; 

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;
            rb2D.velocity = Vector2.zero;
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            StartCoroutine(Grapple());
        }
        else
        {
            canHook = true; 
        }

        yield return new WaitForSeconds(hookCD);
        canHook = true; 
    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }
        line.SetPosition(1, target);
        retracting = true;
    }
}
