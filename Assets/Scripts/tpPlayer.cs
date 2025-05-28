using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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
    public bool canMoveTp = true;
  
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
            isTp1 = true;
          

        }
        if (collision.gameObject.CompareTag("tp2"))
        {

            transform.position = new Vector2(stairs2X, stairs2Y);
            isTp2 = true;
           

        }
       
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tp1"))
        {
            
            isTp1 = false;


        }
        if (collision.gameObject.CompareTag("tp2"))
        {

            
            isTp2 = false;


        }
    }

    private IEnumerator canMoveAfterTp()
    {
        canMoveTp = false;
        yield return new WaitForSeconds(1f);
        canMoveTp = true;
    }
}
