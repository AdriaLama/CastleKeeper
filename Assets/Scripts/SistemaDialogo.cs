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
    public ParedDestructible paredDestructible;

    [TextArea(3, 10)]
    public string[] lineasDialogo = new string[]
  {
    "[Orin se encuentra dentro de la celda]",
    "Orin: Ugh… ¿Dónde… dónde estoy?",
    "Malkior: Bienvenido al final del camino, muchacho. O al principio, dependiendo de qué tan desesperado estés.",
    "Orin: ¿Quién está ahí? ¡Muéstrate!",
    "[De entre las sombras, una cabeza flotante emerge lentamente.]",
    "Malkior: Me llamo Malkior… aunque hace mucho que no me llaman por mi nombre. ¿Y tú? ¿Cómo se llama el temerario que se aventuró en esta fortaleza solo para terminar en una jaula como un ratón?",
    "Orin: Orin… Orin Veldt. Vine aquí por mi hermana. La tienen los vampiros… Fue elegida como sacrificio…",
    "Malkior: (ríe) Oh, qué heroico. Qué predecible. La clásica historia del hermano desesperado. Dime, Orin, ¿estarías dispuesto a perderlo todo para conseguir tu objetivo?",
    "Malkior: Orin. Como puedes ver… no soy precisamente un prisionero común. Fui desmembrado y sellado por el mismo gobernante de los vampiros Azrael Noctros. Pero si me ayudas a recuperar mis partes… puedo darte el poder que necesitas para destruirlos.",
    "Orin: (desconfiado) ¿Por qué debería confiar en ti? Eres un demonio.",
    "Malkior: Si quieres seguir siendo un humano común y corriente, adelante, quédate aquí hasta que te conviertas en el almuerzo de alguien. Pero si realmente quieres salvar a tu hermana… necesitarás más que buenas intenciones. Dime, Orin.",
    "Orin: (dudando) …Si eso me ayuda a salvar a Lina… entonces dime qué tengo que hacer.",
    "Malkior: Buena elección. Primero, rompamos esa pared… y empecemos la cacería."
  };


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
        if (enConversacion && Input.GetKeyDown(KeyCode.E))
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

        // Activar la destrucción de la pared
        if (paredDestructible != null)
        {
            paredDestructible.ActivarDestruccion();
        }
    }
}

