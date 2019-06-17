using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.name == "TutScene")
        {
            Vector3 pos = new Vector3(0, 0, 0);
            //GameObject tutLevel;
            //tutLevel = ;

            GameObject levelPrefab = Instantiate(Resources.Load("Levels/LevelPrefabs/TutorialLevelPrefab"), pos, Quaternion.identity) as GameObject;
            print("loaded tutorial level prefab");
        }
        else
        {
            if (currScene.name != "MainMenu")
            {
                //  Debug.Log("OnSceneLoaded: " + scene.name + " : " + mode);
                int currLevel = PlayerPrefs.GetInt("playPrefsCurrentLevel");

                // print("loaded new level: " + currLevel);

                // if (currLevel != 0)
                // {
                print("loaded game level - not menu : " + currLevel);

                // int levelNumber = PlayerPrefs.GetInt("playPrefsLevelCounter");
                Vector3 pos = new Vector3(0, 0, 0);

                Object[] prefabList;
                prefabList = Resources.LoadAll("Levels/LevelPrefabs", typeof(GameObject));///get list of all prefabs in spawner folder

                GameObject levelPrefab = Instantiate(prefabList[currLevel - 1], pos, Quaternion.identity) as GameObject;
                //   }
                //  print("level: " + levelNumber + " loaded");}
            }
        }
    }
}