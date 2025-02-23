using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPJ : MonoBehaviour
{
 
    public float movSpeed;
    private float movHorizontal;
    private bool lookingRight;
    public float horizontal;


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal, 0, 0) * movSpeed * Time.deltaTime;
    }   

}
