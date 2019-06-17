using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //  [SerializeField]
    public int BoardNumber = 1;

    // public GameObject[] childTilesArray = new GameObject[16];
    public List<GameObject> childTilesList = new List<GameObject>();

    private float movementSpeed = 1f;

    public GameObject GOHolder;
    public GameObject[] sprite_numbers;// 0 is null then 1 is 2 and so forth. 1 = number 2 and 11 is 2048
    public GameObject GridSpot;
    public GameObject turretChosen;
    public GameObject turretActiveButton;
    public GameObject spawningGridPos;
    public GameObject centreGridPos;

    private float GridSpacing = 1f;

    public bool ObjectSelected = false;

    [SerializeField]
    private bool gridActive = false;//use to check if this grid has been activated or not.

    //[SerializeField]
    public bool exportedThisMovement = false;

    public bool movingGrid = false;

    private Vector2 startPos, targetPos, startScale, TargetScale;
    private float movementTimer = 0.5f, t = 0;
    private float UIMovementSpeed = 10f;

    [Header("Script references")]
    public NumGrid CurrentGrid;

    public MainHolderController MainHolder;
    public InputController InpController;
    public LevelManager levelManager;

    public GameObject gridButtonsGO;
    private GameObject currCanvas;
    // public TowerController GridTower;

    // Use this for initialization
    private void Start()
    {
        Initialize();
        CurrentGrid = new NumGrid();
        CurrentGrid = transform.GetComponent<NumGrid>();
        //UnselectGridAtSrtart();
    }

    // Update is called once per frame
    private void Update()
    {
        if (MainHolder.GridFocused == BoardNumber)
        {
            //if the side focused is this grid

            switch (InpController.tempDir)//gets direction to move in from input controller, will be none unless played chooses movement
            {
                case ItemDir.Up:
                    CurrentGrid.MoveGridVertical(ItemDir.Up);
                    exportedThisMovement = false;
                    levelManager.IncrementMovementCounter();
                    break;

                case ItemDir.Down:
                    CurrentGrid.MoveGridVertical(ItemDir.Down);
                    exportedThisMovement = false;
                    levelManager.IncrementMovementCounter();

                    break;

                case ItemDir.Left:
                    //print("moving left" + InpController.tempDir);
                    CurrentGrid.MoveGridHorizontal(ItemDir.Left);
                    exportedThisMovement = false;
                    levelManager.IncrementMovementCounter();

                    break;

                case ItemDir.Right:
                    CurrentGrid.MoveGridHorizontal(ItemDir.Right);
                    exportedThisMovement = false;
                    levelManager.IncrementMovementCounter();

                    break;

                case ItemDir.None: break;
            }
        }

        //if (movingGrid)
        //{
        //   // MoveAndScaleGrid();
        //}
    }

    private void Initialize()
    {
        foreach (Transform child in GOHolder.transform)
        {
            childTilesList.Add(child.gameObject);
        }

        currCanvas = GameObject.Find("Canvas");

        //create number grid
        CreateBoardGrid();
        //ad two numbers to board

        SpawnTwoNewNumbers();
    }

    public void SpawnTwoNewNumbers()
    {
        SpawnNewNumber();
        SpawnNewNumber();
    }

    public void UnselectGrid()
    {
        t = 0;
        //move grid over to turret and change its scale
        movingGrid = true;
        MainHolder.AnimationOccuring = true;
        MainHolder.GridFocused = 0;
        // turretActiveButton.SetActive(true);
        startPos = centreGridPos.transform.position;
        targetPos = spawningGridPos.transform.position;

        //startScale = transform.localScale;
        //TargetScale = new Vector2(0.2f, 0.2f);

        StartCoroutine(MoveFromTo(/*this.transform,*/ startPos, targetPos, UIMovementSpeed));
    }

    //public void UnselectGridAtSrtart()
    //{
    //    print("closing grid");
    //    t = 0;
    //    //move grid over to turret and change its scale
    //   // movingGrid = true;
    //  //  MainHolder.AnimationOccuring = true;
    //    MainHolder.GridFocused = 0;
    //    gridActive = false;
    //    startPos = centreGridPos.transform.position;
    //    targetPos = spawningGridPos.transform.position;
    //    // print(targetPos.x + " " + targetPos.y);[
    //    //startScale = transform.localScale;
    //    //TargetScale = new Vector2(0.2f, 0.2f);
    //   // turretActiveButton.SetActive(true);

    //    //transform.position = targetPos;

    //    //transform.localScale = TargetScale;
    //}

    public void SelectGrid()
    {
        //   print("opening grid: " + this.transform.name);
        t = 0;
        movingGrid = true;
        MainHolder.AnimationOccuring = true;
        //MainHolder.GridFocused = 0;
        gridActive = true;
        startPos = spawningGridPos.transform.position;
        targetPos = centreGridPos.transform.position;

        //startScale = transform.localScale;
        //TargetScale = new Vector2(1, 1);

        StartCoroutine(MoveFromTo(/*this.transform,*/startPos, targetPos, UIMovementSpeed));
    }

    private IEnumerator MoveFromTo(/*Transform objectToMove, */Vector3 a, Vector3 b, float speed)
    {
        MainHolder.AnimationOccuring = true;
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        Vector3 newPos;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            transform.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            newPos = transform.position;
            newPos.z = 3f;
            transform.position = newPos;

            //TODO: convert newpos.y into rectTtrasnform.y then apply to gridButtonsGO.
            Vector2 newPosForGridButtons, movePos;
            float offsetXAxisAmount = -5.6f, offsetYAxisAmount = -1.85f;

            Vector3 offsetPos = new Vector3((newPos.x + offsetXAxisAmount), newPos.y + offsetYAxisAmount, newPos.z);
            //print("grid would be at : " + offsetPos);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(offsetPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(currCanvas.transform as RectTransform, screenPos, Camera.main, out movePos);
            newPosForGridButtons = currCanvas.transform.TransformPoint(movePos);

            gridButtonsGO.transform.position = newPosForGridButtons;
            //

            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }

        // print("mvoefromTo routine finished");
        movingGrid = false;
        MainHolder.AnimationOccuring = false;

        transform.position = b;
        newPos = transform.position;
        newPos.z = 3f;
        transform.position = newPos;
    }

    //private void MoveAndScaleGrid()//called in update, should move grid holder towards turret until close then set bool to false
    //{
    //    print("moving grid");
    //    t += Time.deltaTime / movementTimer;

    //    print("targ: " + targetPos + "start " + startPos + "t " + t);
    //    this.transform.position = targetPos;
    //    //transform.position = Vector2.Lerp(startPos, targetPos, t);
    //   // transform.position = Vector2.Lerp(transform.position, targetPos, t);

    //    //transform.localScale = Vector2.Lerp(startScale, TargetScale, t);

    //    MainHolder.AnimationOccuring = false;

    //    if (Vector2.Distance(transform.position, targetPos) > 0.3f)
    //    {
    //        //continue
    //    }
    //    else
    //    {
    //        movingGrid = false;
    //        t = 0;
    //        //cancel
    //    }
    //}

    private void SpawnNewNumber(int numberValue = 2)// add a new number to the grid
    {
        int randRow, randCol, spareSpotCounter = 0;
        bool FoundEmptySpot = true;//set to false
                                   //pass go to gridspot to change sprite shown
                                   //get random spot on grid, check if it is empty

        foreach (GameObject tile in childTilesList)
        {
            //use to count how many spare spaces there are, only proceed if there are two or more else throw something

            if (tile.GetComponent<GridSpot>().NumberValue == 0)
            {
                //can be used to spawn more numbers
                spareSpotCounter++;
            }
        }
        if (spareSpotCounter >= 2)
        {
            do
            {
                randRow = Random.Range(0, 4);
                randCol = Random.Range(0, 4);
                // print("trying " + randCol + randRow);
                if (CurrentGrid.BoardGrid[randCol, randRow].NumberValue == 0)
                {
                    // print("empty: " + CurrentGrid.BoardGrid[randRow, randCol].name);
                    FoundEmptySpot = false;

                    CurrentGrid.BoardGrid[randCol, randRow].ModifyNumber(GetSuitableSprite(numberValue), numberValue);
                }
            } while (FoundEmptySpot);
        }
        else
        {
            //not enough spots
            NoEnoughSpareSpaces();
        }
    }

    private void NoEnoughSpareSpaces()
    {
        print("not enough spare spaces to dspawn more");
    }

    public GameObject GetSuitableSprite(int value)
    {
        //returns sprite to be sent to change

        switch (value)
        {
            case 0:
                return sprite_numbers[0].gameObject;

            case 2:
                return sprite_numbers[1].gameObject;
            //break;
            case 4:
                return sprite_numbers[2].gameObject;
            // break;
            case 8:
                return sprite_numbers[3].gameObject;
            // break;
            case 16:
                return sprite_numbers[4].gameObject;
            // break;
            case 32:
                return sprite_numbers[5].gameObject;
            // break;
            case 64:
                return sprite_numbers[6].gameObject;
            // break;
            case 128:
                return sprite_numbers[7].gameObject;
            // break;
            case 256:
                return sprite_numbers[8].gameObject;
            // break;
            case 512:
                return sprite_numbers[9].gameObject;
            // break;
            case 1024:
                return sprite_numbers[10].gameObject;
            // break;
            case 2048:
                return sprite_numbers[11].gameObject;
            // break;
            default: break;
        }
        return null;
    }

    private void CreateBoardGrid()
    {
        //create grid of blank objects

        //for (int row = 0; row < 4; row++)
        //{
        //    for (int col = 0; col < 4; col++)
        //    {
        //        float Xpos = col-2, Ypos = row-2;
        //        // GameObject newTile =  Instantiate(GridSpot,new Vector2(col * GridSpacing,row * GridSpacing), Quaternion.identity);
        //        GameObject newTile = Instantiate(GridSpot, new Vector3(col, row,1.234f), Quaternion.identity);
        //        newTile.transform.parent = GOHolder.transform;
        //        newTile.transform.GetComponent<GridSpot>().Initialise(0, col, row, CurrentGrid);

        //        // print("added item" + i + j);
        //    }
        //}

        int colCounter = 0, rowCounter = 0;

        foreach (GameObject tile in childTilesList)
        {
            tile.GetComponent<GridSpot>().Initialise(0, colCounter, rowCounter, CurrentGrid);

            colCounter++;
            if (colCounter == 4)
            {
                colCounter = 0;
                rowCounter++;
            }
        }
    }

    public void ResetGrid()
    {
        foreach (GridSpot spot in CurrentGrid.BoardGrid)
        {
            spot.ResetSpotToDefaults();
        }

        exportedThisMovement = false;
        SpawnTwoNewNumbers();
    }

    public void ExportHighestNumberFromGrid()
    {
        //search grid for hgihest numbers, aedd to list, choose random from list and pass that value to turret and reset that tile
        if (exportedThisMovement) return;
        int highestValue = 0, randValue;
        List<GridSpot> highestGridSpots = new List<GridSpot>();
        GridSpot spotChosen;

        foreach (GridSpot spot in CurrentGrid.BoardGrid)
        {
            if (spot.NumberValue > highestValue)
            {
                //reset list, change highet value
                highestGridSpots.Clear();
                highestGridSpots.Add(spot);
                highestValue = spot.NumberValue;
            }

            if (spot.NumberValue == highestValue)
            {
                //add to list
                highestGridSpots.Add(spot);
            }
        }
        if (highestGridSpots.Count > 1)
        {
            randValue = Random.Range(0, highestGridSpots.Count);
            spotChosen = highestGridSpots[randValue];
        }
        else
        {
            spotChosen = highestGridSpots[1];
        }
        //  print("spot to remove: " + spotChosen);

        // GridTower.shotResources += spotChosen.NumberValue;
        turretChosen.GetComponent<TowerController>().AddResources(spotChosen.NumberValue);
        ResetGridTile(spotChosen);
        exportedThisMovement = true;
        levelManager.IncrementExportCounter();
    }

    private void ResetGridTile(GridSpot spot)
    {
        //takes spot and resets it

        spot.ResetSpotToDefaults();
    }
}