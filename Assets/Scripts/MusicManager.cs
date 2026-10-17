using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;

    private const float MUSIC_VOLUME = 0.1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.mute = PlayerPrefs.GetInt("MusicEnabled", 1) == 0;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SetMusicVolume(musicVolume);

        audioSource.loop = true;

        ChangeMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeMusicForScene(scene.name);
    }

    private void ChangeMusicForScene(string sceneName)
    {
        AudioClip newMusic = null;

        switch (sceneName)
        {
            case "StartMenu":
            case "LevelSelectMenu":
                newMusic = menuMusic;
                break;

            case "Level01":
                newMusic = level1Music;
                break;

            case "Level02":
                newMusic = level2Music;
                break;

            case "Level03":
                newMusic = level3Music;
                break;
        }

        if (newMusic != null)
        {
            PlayBackgroundMusic(newMusic);
        }
    }

    private void PlayBackgroundMusic(AudioClip audioClip)
    {
        if (audioSource.clip == audioClip)
            return;

        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void ToggleMusic(bool enabled)
    {
        audioSource.mute = !enabled;

        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        audioSource.volume = MUSIC_VOLUME * volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}