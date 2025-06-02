using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class SistemaDialogo : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;
    public TextMeshProUGUI nombrePersonaje;

    [Header("Jugador")]
    public MovimientoPJ movimientoPJ;

    [Header("Diálogo")]
    [TextArea(3, 10)]
    public string[] lineasDialogo;

    [Header("Efecto Máquina de Escribir")]
    public float velocidadTexto = 0.02f;
    public AudioClip sonidoTexto;

    private AudioSource audioSource;
    private int indice = 0;
    private bool enConversacion = false;
    private bool textoCompleto = false;
    private Coroutine textoEnCurso;

    void Start()
    {
        panelDialogo.SetActive(false);
        audioSource = GetComponent<AudioSource>();
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
            if (!textoCompleto)
            {
                StopCoroutine(textoEnCurso);
                MostrarLineaInstantanea();
            }
            else
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
    }

    void MostrarLinea()
    {
        if (textoEnCurso != null)
        {
            StopCoroutine(textoEnCurso);
        }

        string linea = lineasDialogo[indice];
        textoEnCurso = StartCoroutine(EscribirTexto(linea));
    }

    IEnumerator EscribirTexto(string linea)
    {
        textoCompleto = false;
        textoDialogo.text = "";
        nombrePersonaje.text = "";

        string textoReal = linea;
        string nombre = "";

        if (linea.Contains(":"))
        {
            string[] partes = linea.Split(new char[] { ':' }, 2);
            nombre = partes[0].Trim();
            textoReal = partes[1].Trim();
            nombrePersonaje.text = nombre;
        }
 
        if (sonidoTexto != null)
        {
            audioSource.clip = sonidoTexto;
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
        }

        for (int i = 0; i < textoReal.Length; i++)
        {
            textoDialogo.text += textoReal[i];
            yield return new WaitForSeconds(velocidadTexto);
        }

        audioSource.Stop();
        audioSource.pitch = 1f;




        textoCompleto = true;
    }

    void MostrarLineaInstantanea()
    {
        textoCompleto = true;

     
        audioSource.Stop();
        audioSource.pitch = 1f;

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

