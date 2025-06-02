using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    private tpPlayer tp;
    public Animator transitionAnimation;
    public GameObject canvas;
    public bool activarHud = false;

    private void Start()
    {
        tp = FindObjectOfType<tpPlayer>();
       

        if (SceneManager.GetActiveScene().name == "PruebaJefe" || SceneManager.GetActiveScene().name == "Sala1" || SceneManager.GetActiveScene().name == "Tutorial")
        {
            transitionAnimation.SetTrigger("Begin");

            StartCoroutine(activarHUD());
        }
      
    }
    public void Play()
    {
        CheckpointManager.Instance.ResetData();
        SceneManager.LoadScene("Sala1");
        Time.timeScale = 1f;
        if (tp != null)
        {
            tp.isTp1 = false;
            tp.isTp2 = false;
        }
      
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
        tp.isTp1 = false;
        tp.isTp2 = false;


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

    public IEnumerator activarHUD()
    {
        yield return new WaitForSeconds(1f);
        activarHud = true;
        canvas.SetActive(true);
    }
}
