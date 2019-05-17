using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelController : MonoBehaviour
{

    [Header("Win Condition arrays")]

    private int[] winBoundariesTimeTaken; //in seconds
    private int[] winBoundariesGridMovements; //amount of swipes used on the grids
    private int[] winBoundariesGridExports; //amount of times a value is exported

    [Header("Actual Completion Variables")]
    private float currentTimeTakenToFinishAllWaves = 0f;
    private int   currentExportCounter = 0;
    private int   currentAmountOfTimesGridMoved = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerEndSplashScreen(int[] winTimeTaken, int[] winGridMoves, int[] winExports,float currTime,int currExport, int currMoves)
    {
        winBoundariesTimeTaken = winTimeTaken;
        winBoundariesGridMovements = winGridMoves;
        winBoundariesGridExports = winExports;
        currentTimeTakenToFinishAllWaves = currTime;
        currentExportCounter = currExport;
        currentAmountOfTimesGridMoved = currMoves;

        //print("end of level Triggered");

    }
    public void TriggerEndLevelBGSplash()
    {
        //activate the end level bg splash
        transform.GetChild(0).transform.gameObject.SetActive(true);

    }
}
