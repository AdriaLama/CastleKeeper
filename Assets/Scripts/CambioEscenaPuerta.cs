using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaPuerta : MonoBehaviour
{
    public string nombreEscena = "Sala1"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}
