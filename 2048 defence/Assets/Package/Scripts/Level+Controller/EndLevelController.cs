using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelController : MonoBehaviour
{
    public GameObject baseImage;
    public GameObject mapGrids;

    public GameObject nextLevelButton;
    public GameObject restartLevelButton;
    [SerializeField]
    private GameObject levelEndSlides;

    public MenuLoader menuLoader;
    public LevelManager levelManager;

    [Header("Win Condition arrays")]
    private int[] winBoundariesTimeTaken; //in seconds, [0] is quickest = 3 stars, [2] is slowest = 1 star

    private int[] winBoundariesGridExports; //amount of times a value is exported
    private int[] winBoundariesGridMovements; //amount of swipes used on the grids

    [Header("Actual Completion Variables")]
    private float currentTimeTakenToFinishAllWaves = 0f;

    private int currentExportCounter = 0;
    private int currentAmountOfTimesGridMoved = 0;

    [Header("Stars")]
    public GameObject leftImage;

    public GameObject MiddleImage;
    public GameObject RightImage;

    [SerializeField]
    private int amountOfStarsEarnt = 0;

    [Header("Other")]
    /// public float[] intHeightVars = new float[] { -999f, -666f, -333f, 0f };//lowest to heighset
    public float[] intHeightVars = new float[] { -251f, -584f, -917f, -1250f };//highest to lowest

    [Header("Testing")]
    public float[] testingValues = new float[] { 75f, 62.5f, 150f };//time taken,grid exports, grid movements

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TriggerEndSplashScreen(int[] winTimeTaken, int[] winGridMoves, int[] winExports, float currTime, int currExport, int currMoves)
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
        DisableObjects();
        transform.GetChild(0).transform.gameObject.SetActive(true);
        ChangeBaseImageColourToEnemyColour(min);
        ActivateEndingSlides();
    }

    public void ChangeBaseImageColourToEnemyColour(GameObject enemy)
    {
        //changes the base image background colour into the colour of the last enemy to prevent sudden colour changes on screen when enemy is deleted

        baseImage.GetComponent<Image>().color = enemy.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void DisableObjects()
    {
        //disables the objects on screen in order to show end level without things moving or showing over
        mapGrids.SetActive(false);
    }

    public void ActivateEndingSlides()
    {
        //trigger the animation on the animator to trigger the slides animation which trigers the staras animation

        Animator anim = transform.GetComponent<Animator>();

        anim.SetTrigger("TriggerMoveEndingSlides");
    }

    private void CalcHeightForTimeTaken(GameObject imageToMove, float currentValueForSection, int[] arrayAssociatedWithCurrValue)//be passed the actual value and the array associated with it
    {
        //use time given and compare to time array and move the according background up cerain amount depending on score percentage

        //  float lowBoundary, highBoundary;// find out what boundarys the score given is between
        float returnHeightToMoveToo;

        //if (currentValueForSection > arrayAssociatedWithCurrValue[3])//is time taken above the LOWEST boundary
        //{
        //    //time taken is above lowest boundary

        //    if (currentValueForSection > arrayAssociatedWithCurrValue[2])//is time taken above the MEDIUM boundary
        //    {
        //        if (currentValueForSection > arrayAssociatedWithCurrValue[1])//is time taken above the HIGHEST boundary
        //        {
        //            print("raising: " + imageToMove.gameObject.name + "with " + currentValueForSection + " to three stars");

        //            returnHeightToMoveToo = intHeightVars[3];
        //            StartCoroutine(RaiseImageGivenByFloatGiven(imageToMove, returnHeightToMoveToo));
        //        }
        //        else
        //        {
        //            print("raising: " + imageToMove.gameObject.name + "with " + currentValueForSection + " to between " + arrayAssociatedWithCurrValue[1] + " and " + arrayAssociatedWithCurrValue[2]);

        //            returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, arrayAssociatedWithCurrValue[1], arrayAssociatedWithCurrValue[2], intHeightVars[2]);
        //            StartCoroutine(RaiseImageGivenByFloatGiven(imageToMove, returnHeightToMoveToo));
        //        }
        //    }
        //    else
        //    {
        //        print("raising: " + imageToMove.gameObject.name + "with " + currentValueForSection + " to between " + arrayAssociatedWithCurrValue[0] + " and " + arrayAssociatedWithCurrValue[1]);

        //        returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, arrayAssociatedWithCurrValue[0], arrayAssociatedWithCurrValue[1], intHeightVars[1]);
        //        StartCoroutine(RaiseImageGivenByFloatGiven(imageToMove, returnHeightToMoveToo));
        //    }
        //}
        //else
        //{
        //    print("raising: " + imageToMove.gameObject.name + "with " + currentValueForSection + " to between 0 and " + arrayAssociatedWithCurrValue[0]);

        //    returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, 0, arrayAssociatedWithCurrValue[0], intHeightVars[0]);
        //    StartCoroutine(RaiseImageGivenByFloatGiven(imageToMove, returnHeightToMoveToo));
        //}

        ///////////////////////////////////////

        if (currentValueForSection < arrayAssociatedWithCurrValue[0])// above 3 stars
        {
            returnHeightToMoveToo = intHeightVars[0];
            // print("raising: " + imageToMove.gameObject.name + "with " + currentValueForSection + " to three stars" + " to between " + arrayAssociatedWithCurrValue[1] + " and " + arrayAssociatedWithCurrValue[2] + " moving filler to height: " + returnHeightToMoveToo);
            amountOfStarsEarnt += 3;
            StartCoroutine(RaiseImageGivenByFloatGiven(imageToMove, returnHeightToMoveToo));
            return;
        }

        if (currentValueForSection <= arrayAssociatedWithCurrValue[1])//on star 3
        {
            returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, arrayAssociatedWithCurrValue[0], arrayAssociatedWithCurrValue[1], intHeightVars[1]);
            // print("raising: " + imageToMove.gameObject.name + "with " + currentValueForSection + " to between " + arrayAssociatedWithCurrValue[1] + " and " + arrayAssociatedWithCurrValue[2] + " moving filler to height: " + returnHeightToMoveToo);
            amountOfStarsEarnt += 2;
            StartCoroutine(RaiseImageGivenByFloatGiven(imageToMove, returnHeightToMoveToo));
            return;
        }

        if (currentValueForSection <= arrayAssociatedWithCurrValue[2])//on star 2
        {
            returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, arrayAssociatedWithCurrValue[1], arrayAssociatedWithCurrValue[2], intHeightVars[2]);
            // print("raising: " + imageToMove.gameObject.name + "with " + currentValueForSection + " to between " + arrayAssociatedWithCurrValue[0] + " and " + arrayAssociatedWithCurrValue[1] + " moving filler to height: " + returnHeightToMoveToo);
            amountOfStarsEarnt += 1;
            StartCoroutine(RaiseImageGivenByFloatGiven(imageToMove, returnHeightToMoveToo));
            return;
        }

        if (currentValueForSection <= arrayAssociatedWithCurrValue[3])//on star 1
        {
            returnHeightToMoveToo = ReturnHeightToRaiseTheImageToo(currentValueForSection, arrayAssociatedWithCurrValue[2], arrayAssociatedWithCurrValue[3], intHeightVars[3]);
            // print("raising: " + imageToMove.gameObject.name + "with " + currentValueForSection + " to between 0 and " + arrayAssociatedWithCurrValue[0] + " moving filler to height: " + returnHeightToMoveToo);
            amountOfStarsEarnt += 0;
            StartCoroutine(RaiseImageGivenByFloatGiven(imageToMove, returnHeightToMoveToo));
            return;
        }
    }
    private float ReturnHeightToRaiseTheImageToo(float actualValue, float lowerBoundaryValue, float higherBoundaryValue, float lowerBoundaryHeight)
    {
        //pass this the score obtained, the lowest boundary it has passed, the next boundary above and the lower boundarys height value

        //takes the actual value obtained and uses the two boundarys it is between to determine the height the image should be raised by depending on the actual value
        //adds that value to the lower boundary ie the height of the star below so its the star belows height plus the percentage of the way through the next stars height

        float varToReturn = 0f, percenToNextStar = 0f;

        percenToNextStar = ((actualValue - lowerBoundaryValue) / (higherBoundaryValue - lowerBoundaryValue)); // returns between 0 -1 range, is % of how far the value is between the boundarys

        //use % obtained above to find out how far to raise the height too between two values

        varToReturn = (percenToNextStar * 333f) + lowerBoundaryHeight;//return between 0-333f + the lower boundary should add up to the toal height it needs to go too, should be how far through next star it needs to be

        // print("testing hegith return value. pecentonextstar = " + percenToNextStar + ", vartoreturn : " + varToReturn + ", lowboundary: " + lowerBoundaryHeight + ",value passed: " + actualValue);

        return varToReturn;
    }

    private IEnumerator RaiseImageGivenByFloatGiven(GameObject imageToRaise, float amountToRaiseToo)//gest given the gameobject to move and the height to raise it too
    {
        //moves the image given to the height given .y in a preset amount of time

        float speed = 2.5f;
        RectTransform rect = imageToRaise.transform.GetComponent<RectTransform>();
        Vector3 targetPos = rect.localPosition;
        targetPos.y = amountToRaiseToo;

        //print("targetpos: " + targetPos + ". currentpos: " + imageToRaise.transform.position);

        float step = (speed / (imageToRaise.transform.position - targetPos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            imageToRaise.transform.localPosition = Vector3.Lerp(imageToRaise.transform.localPosition, targetPos, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }

        imageToRaise.transform.position = targetPos;

        //// RectTransform rect = imageToRaise.transform.GetComponent<RectTransform>();
        ////// print("rect: " + rect.transform.localPosition + " : " + rect.anchoredPosition);
        //// Vector3 newPos = rect.localPosition;
        //// newPos.y = amountToRaiseToo;

        //print("targetpos: " + newPos + ". currentpos: " + rect.transform.localPosition);

        ////imageToRaise.transform.localPosition = newPos;

        yield return new WaitForFixedUpdate();//just here to stop the red wiggly line of annoynace
    }

    public void AnimTriggerActivateLeftImageStars()//left col is time taken
    {
        //currentTimeTakenToFinishAllWaves
        //testingValues[0]

        CalcHeightForTimeTaken(leftImage, currentTimeTakenToFinishAllWaves, winBoundariesTimeTaken);
    }

    public void AnimTriggerActivateMiddleImageStars()//middle is exports used
    {
        //currentExportCounter
        //testingValues[1]

        CalcHeightForTimeTaken(MiddleImage, currentExportCounter, winBoundariesGridExports);
    }

    public void AnimTriggerActivateRightImageStars()//right is grid movements done
    {
        //currentAmountOfTimesGridMoved
        //testingValues[2]

        CalcHeightForTimeTaken(RightImage, currentAmountOfTimesGridMoved, winBoundariesGridMovements);
        
    }
    public void ActivateEndButtons()
    {
        levelManager.ActivateButtons(amountOfStarsEarnt);
    }
    public void ButtonLoadMainMenu()
    {
        menuLoader.LoadMainMenu();
    }

    public void ButtonLoadNextLevel()
    {
        menuLoader.loadNextLevel();
    }
}