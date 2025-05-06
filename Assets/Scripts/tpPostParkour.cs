using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpPostParkour : MonoBehaviour
{
    public Transform player;
    public float stairsX;
    public float stairsY;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = new Vector2(stairsX, stairsY);
        }
    }
}
