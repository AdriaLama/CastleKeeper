using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorFinish : MonoBehaviour
{
    public bool finished = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            finished = true;
            SceneManager.LoadScene("MenuPrincipal");
        }
    }
}
