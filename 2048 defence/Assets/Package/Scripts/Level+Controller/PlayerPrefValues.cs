using UnityEngine;

public class PlayerPrefValues : MonoBehaviour
{
    //list of names used by various other scripts for creation and checking off the PP values
    [Header("Player Prefs values")]
    public static string bPlayPrefsInitialised = "bplayPrefsInitialised";

    public static string iPlayPrefsLevelCounter = "iPlayPrefsLevelCounter";//highest level completed
    //[SerializeField]
    //public static string iPlayPrefsCurrentLevel = "iPlayPrefsCurrentLevel";//current level being loaded

    [Header("Tutorial settings")]
    public static string bPlayPrefstutorialCompleted = "bPlayPrefstutorialCompleted";// 0 if not completed, if 0 then check next value. 1 if completed so mve onto chcking levels

    public static string bPlayPrefstutorialStage = "iplayPrefstutorialCompleted"; //will hold number to mark progress throughout the 3 levels, if 0 then allow only tut 1, if 1 done then allow 2, if 3 done then change above bool

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

    //public static void SetCurrentLevel(int currLevel)
    //{
    //    PlayerPrefs.SetInt(iPlayPrefsCurrentLevel, currLevel);
    //}
}