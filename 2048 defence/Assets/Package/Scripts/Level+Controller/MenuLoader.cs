using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
    public GameObject debugUI;
    public GameObject buttonHolder;
    //public GameObject mainLevelLoadButton;
    public List<Button> buttonsList = new List<Button>();
    private Text debugUIText;


    [Header("Obtained player pref values")]
    [SerializeField]
    private bool bTutorialCompleted = true;
    public int tutorialcurrentStage = -1;
    [SerializeField]

    private int currentLevelCompleted = 0;//-1 means not obtained value yet

    private AudioManager audioMan;
    private LevelMenuManager levelMenuManager;

    // private int  playPrefsCurrentLevel = 0;

    // Use this for initialization
    private void Awake()
    {
        audioMan = GameObject.Find("DontDestroyOnLoad").transform.GetChild(0).gameObject.GetComponent<AudioManager>();
        levelMenuManager = transform.GetComponent<LevelMenuManager>();

        CheckPlayerPrefs();

        // DontDestroyOnLoad(this.transform);
        debugUIText = debugUI.GetComponent<Text>();

        //foreach (Transform child in buttonHolder.transform)
        //{
        //    buttonsList.Add(child.gameObject);
        //    //child.transform.GetComponent<Button>().interactable = false;//todo// re enable when tut is finished. removed to allow easy testing
        //}

        // UpdateMenu();
        UpdateTutorialButtons();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void CheckPlayerPrefs()
    {
        //check if the build has already initalised the needed player prefs values

        if (PlayerPrefs.GetInt(PlayerPrefValues.bPlayPrefsInitialised) == 1)
        {
            print("grabbing player pref valeus already made");
            //has already initalised
            GetAndApplyPlayerPrefs();
        }
        else
        {
            print("initialising pp values");
            InitialisePlayerPrefValues();
        }
        //   UpdateButtons();
    }

    private void GetAndApplyPlayerPrefs()
    {
        //if the values are already made then retreive the values and apply them where neccesary - audio, levels
        bTutorialCompleted = PlayerPrefValues.IntToBoolConvert(PlayerPrefs.GetInt(PlayerPrefValues.bPlayPrefstutorialCompleted));

        if (bTutorialCompleted == false)
        {
            //if tutorial not done then get the tutorial current stage

            tutorialcurrentStage = PlayerPrefs.GetInt(PlayerPrefValues.bPlayPrefstutorialStage);

        }



        currentLevelCompleted = PlayerPrefs.GetInt(PlayerPrefValues.iPlayPrefsLevelCounter);

    }

    private void InitialisePlayerPrefValues()
    {
        //if the build is new then create the set of values needed to store data

        PlayerPrefs.SetInt(PlayerPrefValues.bPlayPrefsInitialised, 1);

        PlayerPrefs.SetInt(PlayerPrefValues.bPlayPrefstutorialCompleted, 1);//TODO  : CHANGE BEFORE RELEASE AS SHOULD BE 0, THIS IS 1 TO ALLOW FOR QUICK TESTINGS
        PlayerPrefs.SetInt(PlayerPrefValues.bPlayPrefstutorialStage, 1);

        PlayerPrefs.SetInt(PlayerPrefValues.iPlayPrefsLevelCounter, 1);
       // PlayerPrefs.SetInt(PlayerPrefValues.iPlayPrefsCurrentLevel, 0);



    }

    private void UpdateTutorialButtons()
    {
            //get the pp values for tutorial and activate or deactivate all the buttons depnding on values obtianed

        if(PlayerPrefs.GetInt(PlayerPrefValues.bPlayPrefstutorialCompleted) == 1)
        {
            print("tut IS done, assigning buttons accordingly");

            //if the tutorial has been completed
            buttonsList[0].interactable = true;
            buttonsList[1].interactable = false;
            buttonsList[2].interactable = false;
            buttonsList[3].interactable = false;


        }
        else
        {
            print("tut NOT done, assigning buttons accordingly");
            //if the tutorial hasnt been completed
            buttonsList[0].interactable = false;
             buttonsList[1].interactable = true;

            int tutLevelsCompleted = PlayerPrefs.GetInt(PlayerPrefValues.bPlayPrefstutorialStage);
            //int tutLevelsCompleted = 2;

            switch (tutLevelsCompleted)
            {
                case 1:
                    //buttonsList[1].interactable = false;
                    buttonsList[2].interactable = true;
                    break;
                case 2:
                   // buttonsList[1].interactable = false;
                    buttonsList[2].interactable = true;
                    buttonsList[3].interactable = true;
                    break;

                case 0:break;
                default: break;

            }


        }

    }

    public void loadNextLevel()
    {
          int levelToLoad = (PlayerPrefs.GetInt(PlayerPrefValues.iPlayPrefsLevelCounter) + 3);//add 3 so when passed to level assigner it loads the game levels, not tut levels as tut levels are numbered 1-3 for this purpose

        LevelLoader(levelToLoad);//loads teh game scene
    }

    public void loadTutorial1()//scene 1 is tutorial, scene 2 is WIP main scene
    {
        int levelToLoad = 1;
        LevelLoader(levelToLoad);
    }
    public void loadTutorial2()//scene 1 is tutorial, scene 2 is WIP main scene
    {
        int levelToLoad = 2;
        LevelLoader(levelToLoad);
    }
    public void loadTutorial3()//scene 1 is tutorial, scene 2 is WIP main scene
    {
        int levelToLoad = 3;
        LevelLoader(levelToLoad);
    }

    private void LevelLoader(int levelToLoad)
    {
        // print("should load tut level" + levelToLoad);
        levelMenuManager.AssignNextLevelToLoad(levelToLoad);

        SceneManager.LoadScene(2);
    }

    public void LoadMainMenu()//save the current level and load the main menu again
    {
        SceneManager.LoadScene(1);
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