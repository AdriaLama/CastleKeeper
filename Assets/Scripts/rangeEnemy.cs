using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class rangeEnemy : MonoBehaviour
{
    public Transform controladorDisparo;
    public float distancialinea;
    public float TiempoDeDisparos;
    public float TiempoDeUltimaBala;
    public GameObject balaEnemigo;
   

    public void Update()
    {
      

            if(Time.time > TiempoDeDisparos + TiempoDeUltimaBala)
            {
                TiempoDeUltimaBala = Time.time;
                Invoke(nameof(Disparar), TiempoDeDisparos);
            }
           
        
    }


    public void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distancialinea);

    }

    public void Disparar()
    {
        Instantiate(balaEnemigo, controladorDisparo.position, controladorDisparo.rotation);
    }

}
