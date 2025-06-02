using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SistemaDialogo : MonoBehaviour

{
    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;
    public TextMeshProUGUI nombrePersonaje;
    public MovimientoPJ movimientoPJ;

    [TextArea(3, 10)]
    public string[] lineasDialogo;

    private int indice = 0;
    private bool enConversacion = false;

    void Start()
    {
        panelDialogo.SetActive(false);
    }

    public void IniciarConversacion()
    {
        enConversacion = true;
        panelDialogo.SetActive(true);
        movimientoPJ.puedeMoverse = false;
        indice = 0;
        MostrarLinea();
    }

    void Update()
    {
        if (!enConversacion) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            indice++;

            if (indice < lineasDialogo.Length)
            {
                MostrarLinea();
            }
            else
            {
                TerminarConversacion();
            }
        }
    }


    void MostrarLinea()
    {
        string linea = lineasDialogo[indice];

        if (linea.Contains(":"))
        {
            string[] partes = linea.Split(new char[] { ':' }, 2);
            nombrePersonaje.text = partes[0].Trim();
            textoDialogo.text = partes[1].Trim();
        }
        else
        {
            nombrePersonaje.text = "";
            textoDialogo.text = linea;
        }
    }


    void TerminarConversacion()
    {
        panelDialogo.SetActive(false);
        enConversacion = false;
        movimientoPJ.puedeMoverse = true;
    }

}
