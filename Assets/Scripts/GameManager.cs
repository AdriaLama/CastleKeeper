using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Celdas");
    }

    public void GoToControlsMenu()
    {
        SceneManager.LoadScene("MenuControles");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

public void Quit()
    {

        Application.Quit();

    }

 
}
