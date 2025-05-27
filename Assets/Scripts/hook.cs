using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private LineRenderer line;
    private Rigidbody2D rb2D;
    private PickItems pi;
    public LayerMask grapplableMask;
    public float maxDistance;
    public float grappleSpeed;
    public float grappleShootSpeed;
    private bool canHook = true;
    public bool isGrappling = false;
    private bool retracting = false;
    private bool hitSomething = false;
    private Vector2 target;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        pi = GetComponent<PickItems>();
        line.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isGrappling && canHook && pi.hasHook)
        {
            StartGrapple();
        }

        if (isGrappling)
        {
            line.SetPosition(0, transform.position + Vector3.up * 1.5f);
        }

        if (retracting)
        {
            transform.position = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
            line.SetPosition(0, transform.position + Vector3.up * 1.5f);
            line.SetPosition(1, target);

            if (Vector2.Distance(transform.position, target) < 1.5f)
            {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
                rb2D.constraints = RigidbodyConstraints2D.None;
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private void StartGrapple()
    {
        canHook = false;
        hitSomething = false;

        Vector2 mouseWorldPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 hookStartPos = (Vector2)transform.position + Vector2.up * 1.5f;
        Vector2 direction = mouseWorldPos - hookStartPos;

        RaycastHit2D hit = Physics2D.Raycast(hookStartPos, direction.normalized, maxDistance, grapplableMask);

        if (hit.collider != null)
        {
            target = hit.point;
            hitSomething = true;
        }
        else
        {
            target = hookStartPos + direction.normalized * maxDistance;
        }

        isGrappling = true;
        line.enabled = true;
        line.positionCount = 2;
        line.SetPosition(0, hookStartPos);
        line.SetPosition(1, hookStartPos);
        StartCoroutine(Grapple());
        canHook = true;
    }

    private IEnumerator Grapple()
    {
        float t = 0;
        float duration = 0.3f;
        Vector2 hookStartPos = (Vector2)transform.position + Vector2.up * 1.5f;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector2 newPos = Vector2.Lerp(hookStartPos, target, t);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);

        if (hitSomething)
        {
            retracting = true;
        }
        else
        {
            isGrappling = false;
            line.enabled = false;
        }
    }
}