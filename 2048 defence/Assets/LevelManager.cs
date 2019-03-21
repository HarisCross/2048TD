using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int LevelNumber = -1;
   // private string playPrefsLevelCounter = "playPrefsLevelCounter";
  //  private string playPrefstutorialCompleted = "playPrefstutorialCompleted";


    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void WaveEnded(SpawnerController spawnController, int waveNumber)
    {
        //wave's minions have all died

        print(spawnController.transform.gameObject + "has ended wave by KO : " + waveNumber);
    }

    public void WaveComplete(SpawnerController spawnController, int waveNumber)
    {
        //wave timer has triggered to end the wave and if poss start next one

        print(spawnController.transform.gameObject + " ended wave by time limit: " + waveNumber);
    }

    public void FinishLevelSuccess()
    {
        //to be called upon wave completion

        PlayerPrefs.SetInt(PlayerPrefValues.playPrefsLevelCounter, LevelNumber);
    }
    public void FinishLevelFail()
    {
        //to be called upon wave completion

     //   PlayerPrefs.SetInt(playPrefsLevelCounter, LevelNumber);
    }
    public void CompleteTutorialLevel()
    {

        PlayerPrefs.SetInt(PlayerPrefValues.playPrefstutorialCompleted, 1);

    }
}