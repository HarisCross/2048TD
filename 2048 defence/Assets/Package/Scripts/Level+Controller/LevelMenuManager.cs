using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    public int nextLevelToLoad = -1;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //public void LoadMainMenuSuccess()
    //{
    //    int levelNumber = PlayerPrefs.GetInt("playPrefsLevelCounter");

    //    PlayerPrefs.SetInt("playPrefsLevelCounter", (levelNumber + 1));
    //    PlayerPrefs.SetInt("playPrefsCurrentLevel", 0);

    //    SceneManager.LoadScene(0);
    //}

    //public void LoadMainMenuFailure()
    //{
    //    PlayerPrefs.SetInt("playPrefsCurrentLevel", 0);

    //    SceneManager.LoadScene(0);
    //}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void AssignNextLevelToLoad(int nextLevel)
    {
        //does what it says on the tin
        nextLevelToLoad = nextLevel;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (nextLevelToLoad == 0)
        {
            LoadNextLevel();
        }
        else
        {
            LoadChosenLevel(nextLevelToLoad);
        }
    }

    public void LoadNextLevel()
    {
        //should load the next level using the current highest level in player prefs
        Scene currScene = SceneManager.GetActiveScene();

        if (currScene.name != "MainMenu")
        {
            int levelToLoad = PlayerPrefs.GetInt(PlayerPrefValues.iPlayPrefsLevelCounter) + 1;

            print("loaded game level - not menu : " + levelToLoad + " using next level");

            // int levelNumber = PlayerPrefs.GetInt("playPrefsLevelCounter");
            Vector3 pos = new Vector3(0, 0, 0);
            Object[] prefabList;

            if (levelToLoad >= 1 && levelToLoad <= 3)
            {
                //if level number is between 1 and 3 then it should load a tutorial level
                prefabList = Resources.LoadAll("Levels/TutorialPrefabs", typeof(GameObject));///get list of all prefabs in spawner folder
            }
            else
            {
                //if level to load is 4 or above then load a normal game level
                prefabList = Resources.LoadAll("Levels/LevelPrefabs", typeof(GameObject));///get list of all prefabs in spawner folder

                levelToLoad -= 3;
            }

            GameObject levelPrefab = Instantiate(prefabList[levelToLoad - 1], pos, Quaternion.identity) as GameObject;
            nextLevelToLoad = 0;
        }
    }

    public void LoadChosenLevel(int ChosenLevel)
    {
        Scene currScene = SceneManager.GetActiveScene();

        if (currScene.name != "MainMenu")
        {
            int currLevel = ChosenLevel;

            print("loaded game level - not menu : " + currLevel + " using chosen level");

            // int levelNumber = PlayerPrefs.GetInt("playPrefsLevelCounter");
            Vector3 pos = new Vector3(0, 0, 0);
            Object[] prefabList;

            if (currLevel >= 1 && currLevel <= 3)
            {
                //if level number is between 1 and 3 then it should load a tutorial level
                prefabList = Resources.LoadAll("Levels/TutorialPrefabs", typeof(GameObject));///get list of all prefabs in spawner folder
            }
            else
            {
                //if level to load is 4 or above then load a normal game level
                prefabList = Resources.LoadAll("Levels/LevelPrefabs", typeof(GameObject));///get list of all prefabs in spawner folder

                currLevel -= 3;//-3 to allow for this value to be used for getting the correct prefab from list
            }

            GameObject levelPrefab = Instantiate(prefabList[currLevel - 1], pos, Quaternion.identity) as GameObject;
            nextLevelToLoad = 0;
        }
    }
}