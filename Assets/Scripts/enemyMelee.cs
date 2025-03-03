using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMelee : MonoBehaviour
{
    public float counter;
    public float waitingTime;
    private bool isRight;
    public float speed;
    
    void Start()
    {
        counter = waitingTime;
    }

    
    void Update()
    {
        if (isRight)
        { 
            transform.position += Vector3.right * speed * Time.deltaTime;
        
        }
       else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

        }
        counter -= Time.deltaTime;




        if (counter <= 0)
        {
            counter = waitingTime;
            isRight = !isRight;
        
        
        
        }





    }
}
