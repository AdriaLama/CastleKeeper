using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class tpPlayer : MonoBehaviour
{
    public GameObject tp1;
    public GameObject tp2;
    public bool isTp1 = false;
    public bool isTp2 = false;
    public float stairsX;
    public float stairsY;
    public float stairs2X;
    public float stairs2Y;
  
    void Start()
    {
      
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tp1"))
        {
            transform.position = new Vector2(stairsX, stairsY);
            
        }
        if (collision.gameObject.CompareTag("tp2"))
        {
            
            transform.position = new Vector2(stairs2X, stairs2Y);
           
        }
    }
}
