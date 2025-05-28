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
       
        if (dashSound != null)
        {
            audioSource.PlayOneShot(dashSound);
        }
     
    }

    public void PlayAttackSound()
    {
       
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
       
    }
}
