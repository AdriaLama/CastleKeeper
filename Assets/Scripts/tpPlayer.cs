using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpPlayer : MonoBehaviour
{
    public GameObject tp1;
    public GameObject tp2;
    public float stairsX;
    public float stairsY;
    public float stairs2X;
    public float stairs2Y;

    void Start()
    {
        
    }

    // Update is called once per frame
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
