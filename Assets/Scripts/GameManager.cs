using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Sala1");
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Celdas");
    }
    public void Quit()
    {

        Application.Quit();

    }

 
}
