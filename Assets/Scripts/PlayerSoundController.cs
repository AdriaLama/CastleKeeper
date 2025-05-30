using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioClip dashSound;
    public AudioClip attackSound;
    public AudioClip heavyAttackSound;
    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;

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

    public void PlayHeavyAttackSound()
    {
        if (heavyAttackSound != null)
        {
            audioSource.PlayOneShot(heavyAttackSound);
        }
    }

    public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void PlayDoubleJumpSound()
    {
        if (doubleJumpSound != null)
        {
            audioSource.PlayOneShot(doubleJumpSound);
        }
    }
}
