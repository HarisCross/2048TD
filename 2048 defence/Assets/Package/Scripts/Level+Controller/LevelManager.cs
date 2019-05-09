using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int LevelNumber = -1;
    // private string playPrefsLevelCounter = "playPrefsLevelCounter";
    //  private string playPrefstutorialCompleted = "playPrefstutorialCompleted";

    [Header("Completion Variables")]
    [Header("Timer")]
    public float currentTimeTakenToFinishAllWaves = 0f;
    [SerializeField]
    private bool timerActiveCountdown = true;//used to stop timer, when level finished change this to stop timer and get time it all ended
    public bool timerCurrentlyActive = false;//used to start timer

    [Header("exports")]
    public int currentExportCounter = 0;

    [Header("grid movements")]
    public int currentAmountOfTimesGridMoved = 0;





    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        print(transform.name);
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
    public void IncrementExportCounter()
    {
        currentExportCounter++;
    }
    public void IncrementMovementCounter()
    {
        currentAmountOfTimesGridMoved++;
    }
    public void AcitvateLevelTimer()
    {
        if (timerCurrentlyActive == false)
        {
            timerCurrentlyActive = true;
            StartCoroutine("LevelTimer");
        }
    }
    private IEnumerator LevelTimer()
    {
        while (timerActiveCountdown)
        {
            yield return new WaitForSeconds(0.01f);
            currentTimeTakenToFinishAllWaves += 0.01f;


        }

    }
    public void ResetLevelScoreCounters()
    {
        currentExportCounter = 0;
        currentAmountOfTimesGridMoved = 0;
        currentTimeTakenToFinishAllWaves = 0f;

    }

}