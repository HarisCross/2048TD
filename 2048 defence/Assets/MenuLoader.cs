using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
    public GameObject debugUI;
    public GameObject buttonHolder;
    private List<GameObject> buttonsList = new List<GameObject>();
    private Text debugUIText;

    private int CurrentMaxLevel = 0;//-1 means not obtained value yet

    private AudioManager audioMan;

    [Header("Player Prefs values")]
    private string playPrefsInitialised = "playPrefsInitialised";
    private string playPrefsLevelCounter = "playPrefsLevelCounter";
    private string playPrefsCurrentLevel = "playPrefsCurrentLevel";
   // private int  playPrefsCurrentLevel = 0;

    // Use this for initialization
    private void Awake()
    {
        audioMan = GameObject.Find("DontDestroyOnLoad").transform.GetChild(0).gameObject.GetComponent<AudioManager>();

        CheckPlayerPrefs();

       // DontDestroyOnLoad(this.transform);
        debugUIText = debugUI.GetComponent<Text>();

        foreach (Transform child in buttonHolder.transform)
        {
            buttonsList.Add(child.gameObject);
            child.transform.GetComponent<Button>().interactable = false;
        }

        // UpdateMenu();
        UpdateButtons();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //private void UpdateMenu()
    //{
    //    if (PlayerPrefs.HasKey(playPrefsLevelCounter))
    //    {
    //        //if there is a saved level counter

    //    }
    //    else
    //    {

    //    }
    //}
    private void CheckPlayerPrefs()
    {
        //check if the build has already initalised the needed player prefs values

        if (PlayerPrefs.HasKey(playPrefsInitialised))
        {
            //has already initalised
            GetAndApplyPlayerPrefs();

        }
        else
        {
            InitialisePlayerPrefValues();

        }
     //   UpdateButtons();

    }
    private void GetAndApplyPlayerPrefs()
    {
        //if the values are already made then retreive the values and apply them where neccesary - audio, levels
        CurrentMaxLevel = PlayerPrefs.GetInt(playPrefsLevelCounter);


        ////audio
        if(PlayerPrefs.GetInt("masterAudioSwitch") != 0)
        {

            audioMan.masterAudioSwitch = true;
        }
        else
        {

            audioMan.masterAudioSwitch = true;

        }

    }
    private void InitialisePlayerPrefValues()
    {
        //if the build is new then create the set of values needed to store data

        PlayerPrefs.SetString(playPrefsInitialised,"true");
        PlayerPrefs.SetInt(playPrefsLevelCounter, CurrentMaxLevel);
        PlayerPrefs.SetInt(playPrefsCurrentLevel, 0);
       // print(PlayerPrefs.GetInt(playPrefsLevelCounter));
       // currentLevel = 0;
       // debugUIText.text = "created level save counter";

        ////audio - 1 for true, 0 for false
        PlayerPrefs.SetInt("masterAudioSwitch",1);
      //  PlayerPrefs.SetInt("masterSFXSwitch",1);
      //  PlayerPrefs.SetInt("masterMusicSwitch",1);

    }
    private void SetCurrentLevel(int currLevel)
    {
        PlayerPrefs.SetInt(playPrefsCurrentLevel, currLevel);

    }
    private void UpdateButtons()
    {
        //UpdateMenu();
        //to be called and run through each button while only activating those which are under the level counter

        //int levelsToBeUnblocked = (currentLevel );
        // print(levelsToBeUnblocked);

        for (int i = 0; i < buttonsList.Count; i++)
        {
            //runs through all of buttons list, sets those not <=levelstobeunlocked to none interactable
           
            if (i < CurrentMaxLevel)
            {
                //unlock 
             //   print("unlocked i: " + i);
                buttonsList[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void loadLevel()
    {
        //int levelToLoad = PlayerPrefs.GetInt("playPrefsCurrentLevel");
        int levelToLoad = 1;
        SetCurrentLevel(levelToLoad);
        LevelLoader(1);//loads teh game scene
    }

    public void loadLevel2()
    {
        int levelToLoad =2;
        SetCurrentLevel(levelToLoad);
        LevelLoader(levelToLoad);
    }

    public void loadLevel3()
    {
        int levelToLoad =3;
        SetCurrentLevel(levelToLoad);
        LevelLoader(levelToLoad);
    }

    private void LevelLoader(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    //pout in func to be called on new scene loaded which uses the pp level number to load the appropriate level prefab

    private void PlayLevelChangeAnimation()
    {
        //called upon scene change, will coevr screen then load next one, needs another screen to play upon scene load to unconver screen
        //load next level when state of animation being played has changed




    }

}