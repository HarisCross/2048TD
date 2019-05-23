using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelController : MonoBehaviour
{

    public GameObject baseImage;
    [SerializeField]
    private GameObject levelEndSlides;


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
    public void TriggerEndLevelBGSplash(GameObject min)
    {
        //activate the end level bg splash
        transform.GetChild(0).transform.gameObject.SetActive(true);
        ChangeBaseImageColourToEnemyColour(min);
        ActivateEndingSlides();
    }

    public void ChangeBaseImageColourToEnemyColour(GameObject enemy)
    {
        //changes the base image background colour into the colour of the last enemy to prevent sudden colour changes on screen when enemy is deleted


        baseImage.GetComponent<Image>().color = enemy.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;

    }

    public void ActivateEndingSlides()
    {
        //actives the ending sldies gameobject which in turn activates the default animations

        levelEndSlides.SetActive(true);

    }
}
