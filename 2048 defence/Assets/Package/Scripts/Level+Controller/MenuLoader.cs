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
    [SerializeField]
    private bool tutorialCompleted = true;

    private int CurrentMaxLevel = 0;//-1 means not obtained value yet

    private AudioManager audioMan;

   // private int  playPrefsCurrentLevel = 0;

    // Use this for initialization
    private void Awake()
    {
        audioMan = GameObject.Find("DontDestroyOnLoad").transform.GetChild(0).gameObject.GetComponent<AudioManager>();

        CheckPlayerPrefs();

       // DontDestroyOnLoad(this.transform);
        debugUIText = debugUI.GetComponent<Text>();

        //foreach (Transform child in buttonHolder.transform)
        //{
        //    buttonsList.Add(child.gameObject);
        //    //child.transform.GetComponent<Button>().interactable = false;//todo// re enable when tut is finished. removed to allow easy testing
        //}

        // UpdateMenu();
        UpdateButtons();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void CheckPlayerPrefs()
    {
        //check if the build has already initalised the needed player prefs values

        if (PlayerPrefs.HasKey(PlayerPrefValues.playPrefsInitialised))
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
        CurrentMaxLevel = PlayerPrefs.GetInt(PlayerPrefValues.playPrefsLevelCounter);
        tutorialCompleted = PlayerPrefValues.IntToBoolConvert(PlayerPrefs.GetInt(PlayerPrefValues.playPrefstutorialCompleted));

        ////audio
        if(PlayerPrefs.GetInt("masterAudioSwitch") != 0)
        {

            audioMan.masterAudioSwitch = true;
        }
        else
        {

            audioMan.masterAudioSwitch = false;

        }

    }  

    private void InitialisePlayerPrefValues()
    {
        //if the build is new then create the set of values needed to store data

        PlayerPrefs.SetInt(PlayerPrefValues.playPrefsInitialised,1);
        PlayerPrefs.SetInt(PlayerPrefValues.playPrefstutorialCompleted, 0);
        PlayerPrefs.SetInt(PlayerPrefValues.playPrefsLevelCounter, CurrentMaxLevel);
        PlayerPrefs.SetInt(PlayerPrefValues.playPrefsCurrentLevel, 0);
       // print(PlayerPrefs.GetInt(playPrefsLevelCounter));
       // currentLevel = 0;
       // debugUIText.text = "created level save counter";
        
        ////audio - 1 for true, 0 for false
        PlayerPrefs.SetInt("masterAudioSwitch",1);
      //  PlayerPrefs.SetInt("masterSFXSwitch",1);
      //  PlayerPrefs.SetInt("masterMusicSwitch",1);

    }

    private void UpdateButtons()
    {
        //UpdateMenu();
        //to be called and run through each button while only activating those which are under the level counter

        //int levelsToBeUnblocked = (currentLevel );
        // print(levelsToBeUnblocked);

        for (int i = 0; i < buttonsList.Count; i++)
        {
           // print(tutorialCompleted + ": tut com");
            //runs through all of buttons list, sets those not <=levelstobeunlocked to none interactable
            if (tutorialCompleted)//only acitvate buttons if ttu has been completed
            {

                if (i < CurrentMaxLevel)
                {
                    //unlock 
                    //   print("unlocked i: " + i);
                    buttonsList[i].GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    public void loadLevel()
    {
        //int levelToLoad = PlayerPrefs.GetInt("playPrefsCurrentLevel");
        int levelToLoad = 1;
        PlayerPrefValues.SetCurrentLevel(levelToLoad);
        LevelLoader(1);//loads teh game scene
    }

    //public void loadLevel2()
    //{
    //    int levelToLoad = 2;
    //    PlayerPrefValues.SetCurrentLevel(levelToLoad);
    //    LevelLoader(levelToLoad);
    //}

    //public void loadLevel3()
    //{
    //    int levelToLoad = 3;
    //    PlayerPrefValues.SetCurrentLevel(levelToLoad);
    //    LevelLoader(levelToLoad);
    //}
    public void loadTutorial()
    {
        //int levelToLoad = 3;
        //PlayerPrefValues.SetCurrentLevel(levelToLoad);
        //LevelLoader(levelToLoad);
        SceneManager.LoadScene(1);
    }
    private void LevelLoader(int levelToLoad)
    {

        //if (tutorialCompleted)
        //{
        //    print("should load normal level" + levelToLoad);
        //    SceneManager.LoadScene(2);
        //}
        //else
        //{
            print("should load tut level" + levelToLoad);
            SceneManager.LoadScene(2);
      //  }

    }

    //pout in func to be called on new scene loaded which uses the pp level number to load the appropriate level prefab

    private void PlayLevelChangeAnimation()
    {
        //called upon scene change, will coevr screen then load next one, needs another screen to play upon scene load to unconver screen
        //load next level when state of animation being played has changed




    }

    public void HideButtonHolderGO(GameObject buttonHold)
    {
        //trigger animator on button holder to have it move down and hide so its not on screen during end level

        buttonHolder = buttonHold;

        Animator anim = buttonHolder.transform.GetComponent<Animator>();
        anim.SetTrigger("HideButtonHolderTrigger");

    }


}