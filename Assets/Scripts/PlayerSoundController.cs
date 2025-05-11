using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioClip dashSound;
    public AudioClip attackSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void PlayDashSound()
    {
        Debug.Log("Dash sound trigger");
        if (dashSound != null)
        {
            audioSource.PlayOneShot(dashSound);
        }
        else
        {
            Debug.LogWarning("dashSound no asignado.");
        }
    }

    public void PlayAttackSound()
    {
        Debug.Log("Attack sound trigger");
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
        else
        {
            Debug.LogWarning("attackSound no asignado.");
        }
    }
}
