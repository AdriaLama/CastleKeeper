using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerControler : MonoBehaviour
{
    private Animator animator;
    private MovimientoPJ pj;
    private RangeAttack ra;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pj = GetComponent <MovimientoPJ>();
        ra = FindObjectOfType <RangeAttack>();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (pj.horizontal > 0 && pj.checkGroundLineCast() || pj.horizontal < 0 && pj.checkGroundLineCast())
        {
            animator.SetBool("Walk", true);

        }
        else
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Q) && ra.canAttack)
        {
            animator.SetBool("Attack", true);
        }
        if (!ra.canAttack)
        { 
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
        }


    }
}
