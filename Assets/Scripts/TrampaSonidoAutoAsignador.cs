using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaSonidoAutoAsignador : MonoBehaviour
{
    public AudioClip sonidoDesaparecer;

    void Start()
    {
        trampaDesaparecer[] trampas = FindObjectsOfType<trampaDesaparecer>();
        int contador = 0;

        foreach (var trampa in trampas)
        {
            AudioSource audioSource = trampa.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = trampa.gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
            }

            trampa.sonidoDesaparecer = sonidoDesaparecer;
            contador++;
        }

        Debug.Log("Asignado sonido a " + contador + " trampas.");
    }
}
