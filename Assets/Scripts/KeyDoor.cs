using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class KeyDoor : MonoBehaviour
{
    public GameObject doorKey;
    public bool hasKey = false;
    public GameObject key;

    public AudioClip sonidoRecogerLlave;
    public AudioClip sonidoAbrirPuerta;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!hasKey)
            {
                hasKey = true;
                key.SetActive(true);

                if (sonidoRecogerLlave != null)
                    audioSource.PlayOneShot(sonidoRecogerLlave);

                Destroy(gameObject); 
            }
            else
            {
                if (doorKey != null)
                {
                    if (sonidoAbrirPuerta != null)
                        audioSource.PlayOneShot(sonidoAbrirPuerta);

                    Destroy(doorKey); 
                }
            }
        }
    }
}
