using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    public Toggle musicCheckbox;
    public Slider musicVolumeSlider;
    public TextMeshProUGUI musicVolumeLabel;
    public Image musicSliderBackground;
    public Image musicSliderFill;
    public Image musicSliderHandle;

    public Toggle soundFXCheckbox;
    public Slider soundFXVolumeSlider;
    public TextMeshProUGUI soundFXVolumeLabel;
    public Image soundFXSliderBackground;
    public Image soundFXSliderFill;
    public Image soundFXSliderHandle;

    [Header("Reset Progress")]
    public GameObject resetProgressButton;
    [SerializeField] private MessageModalController resetSuccessMessageModalController;

    void Start()
    {
        // audio
        StartMusicSettings();
        StartSoundFXSettings();

        // reset progress
        resetProgressButton.SetActive(false);
    }

    public void ShowResetProgress()
    {
        HideAll();

        resetProgressButton.SetActive(true);
    }

    public void ShowAudioSettings()
    {
        HideAll();

        musicCheckbox.gameObject.SetActive(true);
        musicVolumeSlider.gameObject.SetActive(true);
        musicVolumeLabel.gameObject.SetActive(true);
        
        soundFXCheckbox.gameObject.SetActive(true);
        soundFXVolumeSlider.gameObject.SetActive(true);
        soundFXVolumeLabel.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        // audio
        musicCheckbox.gameObject.SetActive(false);
        musicVolumeSlider.gameObject.SetActive(false);
        musicVolumeLabel.gameObject.SetActive(false);

        soundFXCheckbox.gameObject.SetActive(false);
        soundFXVolumeSlider.gameObject.SetActive(false);
        soundFXVolumeLabel.gameObject.SetActive(false);

        // reset progress
        resetProgressButton.SetActive(false);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("HighestUnlockedLevel");
        PlayerPrefs.DeleteKey("HasDoubleJump");
        PlayerPrefs.DeleteKey("HasWallJump");
        PlayerPrefs.DeleteKey("TotalDeaths");

        PlayerPrefs.Save();

        resetSuccessMessageModalController.Open();
    }

    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void ToggleMusic(bool enabled)
    {
        Color enabledColor;
        Color disabledColor;
        Color backgroundEnabledColor;
        Color backgroundDisabledColor;

        MusicManager.Instance.ToggleMusic(enabled);
        musicVolumeSlider.interactable = enabled;
        UpdateMusicCheckboxNavigation();

        ColorUtility.TryParseHtmlString("#FFFFFF", out enabledColor);
        ColorUtility.TryParseHtmlString("#333333", out disabledColor);
        ColorUtility.TryParseHtmlString("#8F8F8F", out backgroundEnabledColor);
        ColorUtility.TryParseHtmlString("#0F0F0F", out backgroundDisabledColor);

        var generalColor = enabled ? enabledColor : disabledColor;
        var backgroundColor = enabled ? backgroundEnabledColor : backgroundDisabledColor;

        musicSliderBackground.color = backgroundColor;
        musicSliderFill.color = generalColor;
        musicSliderHandle.color = generalColor;
        musicVolumeLabel.color = generalColor;
    }

    public void ToggleSoundFX(bool enabled)
    {
        Color enabledColor;
        Color disabledColor;
        Color backgroundEnabledColor;
        Color backgroundDisabledColor;

        SFXManager.Instance.ToggleSoundFX(enabled);
        soundFXVolumeSlider.interactable = enabled;
        UpdateSoundFXCheckboxNavigation();

        ColorUtility.TryParseHtmlString("#FFFFFF", out enabledColor);
        ColorUtility.TryParseHtmlString("#333333", out disabledColor);
        ColorUtility.TryParseHtmlString("#8F8F8F", out backgroundEnabledColor);
        ColorUtility.TryParseHtmlString("#0F0F0F", out backgroundDisabledColor);

        var generalColor = enabled ? enabledColor : disabledColor;
        var backgroundColor = enabled ? backgroundEnabledColor : backgroundDisabledColor;

        soundFXSliderBackground.color = backgroundColor;
        soundFXSliderFill.color = generalColor;
        soundFXSliderHandle.color = generalColor;
        soundFXVolumeLabel.color = generalColor;
    }

    private void StartMusicSettings()
    {
        musicCheckbox.gameObject.SetActive(false);
        musicVolumeSlider.gameObject.SetActive(false);
        musicVolumeLabel.gameObject.SetActive(false);

        musicCheckbox.SetIsOnWithoutNotify(
            PlayerPrefs.GetInt("MusicEnabled", 1) == 1
        );

        musicVolumeSlider.SetValueWithoutNotify(
            PlayerPrefs.GetFloat("MusicVolume", 1f)
        );

        musicVolumeLabel.text = Mathf.RoundToInt(musicVolumeSlider.value * 100) + "%";
        ToggleMusic(musicCheckbox.isOn);
    }

    private void StartSoundFXSettings()
    {
        soundFXCheckbox.gameObject.SetActive(false);
        soundFXVolumeSlider.gameObject.SetActive(false);
        soundFXVolumeLabel.gameObject.SetActive(false);

        soundFXCheckbox.SetIsOnWithoutNotify(
            PlayerPrefs.GetInt("SoundFXEnabled", 1) == 1
        );

        soundFXVolumeSlider.SetValueWithoutNotify(
            PlayerPrefs.GetFloat("SoundFXVolume", 1f)
        );

        soundFXVolumeLabel.text = Mathf.RoundToInt(soundFXVolumeSlider.value * 100) + "%";
        ToggleSoundFX(soundFXCheckbox.isOn);
    }

    public void ChangeMusicVolume(float value)
    {
        MusicManager.Instance.SetMusicVolume(value);
        musicVolumeLabel.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void ChangeSoundFXVolume(float value)
    {
        SFXManager.Instance.SetSoundFXVolume(value);
        soundFXVolumeLabel.text = Mathf.RoundToInt(value * 100) + "%";
    }

    
    private void UpdateMusicCheckboxNavigation()
    {
        bool enabled = musicVolumeSlider.IsInteractable();

        var musicNav = musicCheckbox.navigation;
        musicNav.mode = Navigation.Mode.Explicit;
        musicNav.selectOnRight = enabled
            ? musicVolumeSlider
            : null;

        musicCheckbox.navigation = musicNav;
    }

    private void UpdateSoundFXCheckboxNavigation()
    {
        bool enabled = soundFXVolumeSlider.IsInteractable();

        var soundNav = soundFXCheckbox.navigation;
        soundNav.mode = Navigation.Mode.Explicit;
        soundNav.selectOnRight = enabled
            ? soundFXVolumeSlider
            : null;

        soundFXCheckbox.navigation = soundNav;
    }
}