using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    private AudioSource audioSource;

    [Header("SFX")]
    public AudioClip deathSound;
    public AudioClip jumpSound;
    public AudioClip checkpointSound;
    public AudioClip upgradeSound;
    public AudioClip doorSound;

    private const float SFX_VOLUME = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.mute = PlayerPrefs.GetInt("SoundFXEnabled", 1) == 0;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, GetSoundFXVolume());
        }
    }

    public void ToggleSoundFX(bool enabled)
    {
        audioSource.mute = !enabled;

        PlayerPrefs.SetInt("SoundFXEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private float GetSoundFXVolume()
    {
        return PlayerPrefs.GetFloat("SoundFXVolume", 1f);
    }

    public void SetSoundFXVolume(float volume)
    {
        audioSource.volume = SFX_VOLUME * volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}