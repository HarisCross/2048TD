using UnityEngine;

public class PlayerPrefValues : MonoBehaviour
{
    [Header("Player Prefs values")]
    public static string playPrefsInitialised = "playPrefsInitialised";

    public static string playPrefsLevelCounter = "playPrefsLevelCounter";
    public static string playPrefsCurrentLevel = "playPrefsCurrentLevel";
    public static string playPrefstutorialCompleted = "playPrefstutorialCompleted";

    public static bool IntToBoolConvert(int val)
    {
        switch (val)
        {
            case 1: return true;
            case 0: return false;
        }
        return false;
    }

    public static int BoolToIntConvert(bool val)
    {
        if (val)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public static void SetCurrentLevel(int currLevel)
    {
        PlayerPrefs.SetInt(playPrefsCurrentLevel, currLevel);
    }
}