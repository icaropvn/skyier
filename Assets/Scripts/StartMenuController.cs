using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    public TextMeshProUGUI versionLabel;

    void Start()
    {
        SetVersionLabel();
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("LevelSelectMenu");
    }

    public void OnSettingsClick()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void OnExitClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    public void SetVersionLabel()
    {
        versionLabel.text = "v" + Application.version;
    }
}
