using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class InicioEvento : MonoBehaviour
{
    public SistemaDialogo sistemaDialogo;

    void Start()
    {
        StartCoroutine(EsperarYEjecutar());
    }

    IEnumerator EsperarYEjecutar()
    {
        yield return new WaitForSeconds(2.5f);
        sistemaDialogo.IniciarConversacion();
    }
}

