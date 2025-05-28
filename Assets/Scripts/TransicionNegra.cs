using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransicionNegra : MonoBehaviour
{
    private tpPlayer tp;
    public Animator transicionAnimacion;


    void Start()
    {
        tp = FindObjectOfType<tpPlayer>();
    }

    void Update()
    {

        if (tp.isTp1)
        {
            transicionAnimacion.SetTrigger("Begin");
          
        }
        else if (tp.isTp2)
        {
            transicionAnimacion.SetTrigger("Begin");
            
        }
    }

 

}



