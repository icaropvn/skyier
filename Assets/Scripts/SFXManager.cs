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
    public AudioClip menuNavigationSound;
    public AudioClip menuConfirmSound;

    private const float SFX_VOLUME = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.mute = PlayerPrefs.GetInt("SoundFXEnabled", 1) == 0;
            audioSource.volume = SFX_VOLUME * PlayerPrefs.GetFloat("SoundFXVolume", 1f);

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
            audioSource.PlayOneShot(clip);
        }
    }

    public void ToggleSoundFX(bool enabled)
    {
        audioSource.mute = !enabled;

        PlayerPrefs.SetInt("SoundFXEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetSoundFXVolume(float volume)
    {
        audioSource.volume = SFX_VOLUME * volume;
        PlayerPrefs.SetFloat("SoundFXVolume", volume);
    }
}