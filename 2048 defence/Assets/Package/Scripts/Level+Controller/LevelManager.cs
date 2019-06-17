using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int LevelNumber = -1;
    public EndLevelController endLevelController;
    public MenuLoader menuLoader;
    public GameObject buttonHolder;
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

    [Header("spawner info")]
    public int amountOfSpawners;

    public int counterSpawnersCompleted = 0;

    [Header("Win Condition arrays")]
    public int[] winBoundariesTimeTaken; //in seconds

    public int[] winBoundariesGridMovements; //amount of swipes used on the grids
    public int[] winBoundariesGridExports; //amount of times a value is exported

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

    public void LevelCompeletion()
    {
        if (counterSpawnersCompleted == amountOfSpawners)
        {
            //all spawners have finihsed thier waves hence the level is

            // print("THE LEVEL IS OVER");
            endLevelController.TriggerEndSplashScreen(winBoundariesTimeTaken, winBoundariesGridMovements, winBoundariesGridExports, currentTimeTakenToFinishAllWaves, currentExportCounter, currentAmountOfTimesGridMoved);
        }
    }

    public void TriggerEndLevelBGSplash(GameObject min)
    {
        endLevelController.TriggerEndLevelBGSplash(min);
    }

    public void TriggerEndLevelStartOfAnim()
    {
        menuLoader.HideButtonHolderGO(buttonHolder);
    }

    public void WaveComplete(SpawnerController spawnController, int waveNumber)
    {
        //wave timer has triggered to end the wave and if poss start next one

        //  print(spawnController.transform.gameObject + " ended wave by time limit: " + waveNumber);
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
        float tempFloat;
        while (timerActiveCountdown)
        {
            yield return new WaitForSeconds(0.025f);
            tempFloat = currentTimeTakenToFinishAllWaves += 0.025f;
            //  currentTimeTakenToFinishAllWaves += 0.1f;

            //System.Math.Round(currentTimeTakenToFinishAllWaves, 2);

            currentTimeTakenToFinishAllWaves = (float)System.Math.Round(tempFloat, 2);
        }
    }

    public void ResetLevelScoreCounters()
    {
        currentExportCounter = 0;
        currentAmountOfTimesGridMoved = 0;
        currentTimeTakenToFinishAllWaves = 0f;
    }
}