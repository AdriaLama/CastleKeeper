using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    public void Play()
    {
        SceneManager.LoadScene("Sala1");
        Time.timeScale = 1f;
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        CheckpointManager.Instance.ResetData(); 
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void Quit()
    {

        Application.Quit();

    }

    public void Update()
    {
        
            if (Input.GetButtonDown("Cancel"))
            {
                if (SceneManager.GetActiveScene().name == "Sala1" || SceneManager.GetActiveScene().name == "Tutorial" || SceneManager.GetActiveScene().name == "PruebaJefe")
                {
                    isPaused = !isPaused;
                    if (isPaused)
                    {
                        Paused();
                    }
                    else if (!isPaused)
                    {
                        Continue();
                    }

                }
            }
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

}
