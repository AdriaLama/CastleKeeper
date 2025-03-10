using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
    LineRenderer line;
    public LayerMask grapplableMask;
    public float maxDistance;
    public float grappleSpeed;
    public float grappleShootSpeed;
    public bool isGrappling = false;
    public bool retracting = false;
    Vector2 target;
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isGrappling) 
        {
            StartGrapple();
        
        
        
        }
        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);                      
            transform.position = grapplePos;


            if (Vector2.Distance(transform.position, target) < 1f)
            {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            
            }


        }
    }
    private void StartGrapple()
    { 
       Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);
    
        if (hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    
        
    
    
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
