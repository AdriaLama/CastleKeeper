using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToControlsMenu()
    {
        SceneManager.LoadScene("MenuControles");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
