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

    public void LoadMainMenuSuccess()
    {
        int levelNumber = PlayerPrefs.GetInt("playPrefsLevelCounter");

        PlayerPrefs.SetInt("playPrefsLevelCounter", (levelNumber + 1));

        SceneManager.LoadScene(0);
    }

    public void LoadMainMenuFailure()
    {




        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      //  Debug.Log("OnSceneLoaded: " + scene.name + " : " + mode);

        int levelNumber = PlayerPrefs.GetInt("playPrefsLevelCounter");
        Vector3 pos = new Vector3(0, 0, 0);

        Object[] prefabList;
        prefabList = Resources.LoadAll("Levels/LevelPrefabs", typeof(GameObject));///get list of all prefabs in spawner folder

        GameObject levelPrefab = Instantiate(prefabList[levelNumber-1], pos, Quaternion.identity) as GameObject;

        //  print("level: " + levelNumber + " loaded");
    }
}