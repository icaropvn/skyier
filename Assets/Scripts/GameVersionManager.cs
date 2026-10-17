using UnityEngine;

public static class GameVersionManager
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CheckVersion()
    {
        string currentVersion = Application.version;
        string savedVersion = PlayerPrefs.GetString("GameVersion", "");

        if (savedVersion != currentVersion)
        {
            PlayerPrefs.DeleteAll();

            PlayerPrefs.SetString("GameVersion", currentVersion);
            PlayerPrefs.Save();
        }
    }
}