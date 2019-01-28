using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour {

    public GameObject debugUI;
    public GameObject buttonHolder;
        private List<GameObject> buttonsList = new List<GameObject>();
    private Text debugUIText;

    int currentLevel = -1;//-1 means not obtained value yet

    string playPrefsLevelCounter = "playPrefsLevelCounter";

    // Use this for initialization
    void Start () {
        debugUIText = debugUI.GetComponent<Text>();

        foreach (Transform child in buttonHolder.transform)
        {
            buttonsList.Add(child.gameObject);
            child.transform.GetComponent<Button>().interactable = false;
        }


        UpdateMenu();
       
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateMenu()
    {

        if (PlayerPrefs.HasKey(playPrefsLevelCounter))
        {
            //if there is a saved level counter
            currentLevel = PlayerPrefs.GetInt(playPrefsLevelCounter);
            debugUIText.text = "loaded level save counter";
        }
        else
        {
            PlayerPrefs.SetInt(playPrefsLevelCounter, 0);
            debugUIText.text = "created level save counter";

        }
        UpdateButtons();

    }
    void UpdateButtons()
    {
        //UpdateMenu();
        //to be called and run through each button while only activating those which are under the level counter

        int levelsToBeUnblocked = (currentLevel + 1);
       // print(levelsToBeUnblocked);

        for(int i = 0; i < buttonsList.Count; i++)
        {
            //runs through all of buttons list, sets those not <=levelstobeunlocked to none interactable

            if(i<=levelsToBeUnblocked)
            {
                //unlock
                buttonsList[i].GetComponent<Button>().interactable = true;
            } 

        }



    }
    public void loadLevel1() 
    {
            int levelToLoad = 1;
        LevelLoader(levelToLoad);

    }
    public void loadLevel2()
    {
        int levelToLoad = 3;
        LevelLoader(levelToLoad);

    }
    public void loadLevel3()
    {
        int levelToLoad = 2;
        LevelLoader(levelToLoad);

    }
    private void LevelLoader(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
