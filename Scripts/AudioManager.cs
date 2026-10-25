using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    [Header("Sound Effects")]
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip gameOverSound;
    public AudioClip powerUpSound;
    public AudioClip rockShowerSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
    }

    public void PlayGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }
    public void PlayRockShower()
    {
        sfxSource.PlayOneShot(rockShowerSound);
    }
    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayJump()
    {
        sfxSource.PlayOneShot(jumpSound);
    }

    public void PlayCoin()
    {
        sfxSource.PlayOneShot(coinSound);
    }

    public void PlayGameOver()
    {
        musicSource.Stop();
        sfxSource.PlayOneShot(gameOverSound);
    }

    public void PlayPowerUp()
    {
        sfxSource.PlayOneShot(powerUpSound);
    }
}