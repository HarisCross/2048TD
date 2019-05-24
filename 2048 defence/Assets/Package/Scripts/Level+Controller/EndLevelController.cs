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

    private int[] winBoundariesTimeTaken; //in seconds, [0] is quickest = 3 stars, [2] is slowest = 1 star
    private int[] winBoundariesGridExports; //amount of times a value is exported
    private int[] winBoundariesGridMovements; //amount of swipes used on the grids

    [Header("Actual Completion Variables")]
    private float currentTimeTakenToFinishAllWaves = 0f;
    private int   currentExportCounter = 0;
    private int   currentAmountOfTimesGridMoved = 0;

    [Header("Images to Move For Stars")]
    public GameObject leftImage;
    public GameObject MiddleImage;
    public GameObject RightImage;

    [Header("Other")]

    public float[] intHeightVars =  new float[] {-999f,-666f,-333f,0f };//lowest to heighset

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

    private void CalcHeightForTimeTaken(float currentValueForSection,int[] arrayAssociatedWithCurrValue)//be passed the actual value and the array associated with it
    {
        //use time given and compare to time array and move the according background up cerain amount depending on score percentage

        float lowBoundary, highBoundary;// find out what boundarys the score given is between
        float returnHeightToMoveToo;

        if(currentValueForSection < arrayAssociatedWithCurrValue[2])//is time taken above the LOWEST boundary
        {
            //time taken is above lowest boundary

            if (currentValueForSection < arrayAssociatedWithCurrValue[1])//is time taken above the MEDIUM boundary
            {
                if (currentValueForSection < arrayAssociatedWithCurrValue[0])//is time taken above the HIGHEST boundary
                {
                    //above 3 stars###, doesnt need to find amount to raise as it will fill all stars anyway

                    returnHeightToMoveToo = intHeightVars[3];


                    StartCoroutine(RaiseImageGivenByFloatGiven ( leftImage, returnHeightToMoveToo));

                }
                else
                {
                    //between 2 and 3 stars#####needs to find amount to raise image by##

                    returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, arrayAssociatedWithCurrValue[1], arrayAssociatedWithCurrValue[2],intHeightVars[2], intHeightVars[3]);
                    StartCoroutine(RaiseImageGivenByFloatGiven(leftImage, returnHeightToMoveToo));
                }

            }
            else
            {
                //between 1 and 2 stars#####needs to find amount to raise image by##
                returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, arrayAssociatedWithCurrValue[0], arrayAssociatedWithCurrValue[1],intHeightVars[1], intHeightVars[2]);
                StartCoroutine(RaiseImageGivenByFloatGiven(leftImage, returnHeightToMoveToo));
            }


        }
        else
        {
            //time taken is BELOW lowest boundary,##needs to find amount to raise image by##
            returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, 0, arrayAssociatedWithCurrValue[0],intHeightVars[0],intHeightVars[1]);
            StartCoroutine(RaiseImageGivenByFloatGiven(leftImage, returnHeightToMoveToo));
        }

    }
    private float ReturnHeightToRaiseTheImageToo(float actualValue,float lowerBoundaryValue,float higherBoundaryValue,float lowerBoundaryHeight, float higherBoundaryHeight)
    {
        //takes the actual value obtained and uses the two boundarys it is between to determine the height the image should be raised by depending on the actual value

        float varToReturn = 0f;

        varToReturn = (actualValue - lowerBoundaryValue) / (higherBoundaryValue - lowerBoundaryValue); // returns between 0 -1 range, is % of how far the value is between the boundarys

        //use % obtained above to find out how far to raise the height too between two values


        return varToReturn;
    }
    private IEnumerator RaiseImageGivenByFloatGiven(GameObject imageToRaise,float amountToRaiseToo)
    {
        yield return new WaitForEndOfFrame();//just here to stop the red wiggly line of annoynace
    }
}
