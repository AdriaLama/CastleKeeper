using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PopOutTrigger2D : MonoBehaviour
{
    public GameObject popOutText; // Objeto de texto que aparecerá
    private bool isPlayerInside = false; // Bandera para verificar si el jugador está en la zona

    private void Start()
    {
        if (popOutText != null)
        {
            popOutText.SetActive(false); // Oculta el mensaje al iniciar
        }
        else
        {
            Debug.LogError("El objeto popOutText no ha sido asignado en el Inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Detecta cuando el jugador entra en el trigger
    {
        if (other.CompareTag("Player")) // Verifica que el objeto tenga el tag "Player"
        {
            Debug.Log("Jugador detectado en el trigger.");
            popOutText.SetActive(true); // Muestra el panel de texto cuando el jugador entra
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Detecta cuando el jugador sale del trigger
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador salió del trigger.");
            isPlayerInside = false; // Marca que el jugador ha salido del trigger, pero no oculta el panel todavía
        }
    }

    private void Update()
    {
        // Verifica si el jugador presiona la tecla F y oculta el panel
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Tecla F presionada. Ocultando mensaje.");
            popOutText.SetActive(false); // Oculta el mensaje
        }

        // Si el jugador está dentro del trigger, el panel permanece visible
        if (isPlayerInside && !popOutText.activeSelf)
        {
            popOutText.SetActive(true); // Asegura que el texto esté visible si el jugador está dentro del trigger
        }
    }
}