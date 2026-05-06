using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip chaseMusic;
    public AudioClip victorySound;
    public AudioClip gameOverSound;
    public AudioClip pickupSound;
    public AudioClip slowSound;
    public AudioClip footstepsSound;

    private bool footstepsPlaying = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        musicSource.clip = chaseMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayPickup()
    {
        sfxSource.PlayOneShot(pickupSound, 1.3f);
    }

    public void PlaySlow()
    {
        sfxSource.PlayOneShot(slowSound, 0.5f);
    }

    public void PlayVictory()
    {
        musicSource.Stop();
        sfxSource.PlayOneShot(victorySound);
    }

    public void PlayGameOver()
    {
        musicSource.Stop();
        sfxSource.PlayOneShot(gameOverSound);
    }

    public void StartFootsteps()
    {
        if (!footstepsPlaying)
        {
            footstepsPlaying = true;
            sfxSource.clip = footstepsSound;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    public void StopFootsteps()
    {
        if (footstepsPlaying)
        {
            footstepsPlaying = false;
            sfxSource.Stop();
            sfxSource.loop = false;
        }
    }
}