using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesManager : MonoBehaviour
{
    public Slider volumenMusicaSlider;
    public Slider volumenEfectosSlider;

    void Start()
    {
        
        float volMusica = PlayerPrefs.GetFloat("volumenMusica", 1f);
        float volEfectos = PlayerPrefs.GetFloat("volumenEfectos", 1f);

        volumenMusicaSlider.value = volMusica;
        volumenEfectosSlider.value = volEfectos;

        
        AudioListener.volume = volMusica; 
    }

    public void CambiarVolumenMusica(float valor)
    {
        PlayerPrefs.SetFloat("volumenMusica", valor);
        AudioListener.volume = valor; 
    }

    public void CambiarVolumenEfectos(float valor)
    {
        PlayerPrefs.SetFloat("volumenEfectos", valor);
        // Aquí también actualizarías un AudioMixer si lo usas
    }
}
