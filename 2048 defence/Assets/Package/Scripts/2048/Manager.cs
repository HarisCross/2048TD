using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private int BoardNumber = 1;

    private float movementSpeed = 1f;

    public GameObject GOHolder;
    public GameObject[] sprite_numbers;// 0 is null then 1 is 2 and so forth. 1 = number 2 and 11 is 2048
    public GameObject GridSpot;
    public GameObject turretChosen;
    public GameObject turretActiveButton;
    public GameObject centreGridSpot;

    private float GridSpacing = 1f;

    public bool ObjectSelected = false;

    [SerializeField]
    private bool exportedThisMovement = false;

    public bool movingGrid = false;

    private Vector2 startPos, targetPos, startScale, TargetScale;
    private float movementTimer = 1f, t = 0;

    [Header("Script references")]
    public NumGrid CurrentGrid;

    public MainHolderController MainHolder;
    public InputController InpController;
    // public TowerController GridTower;

    // Use this for initialization
    private void Start()
    {
        Initialize();
        CurrentGrid = new NumGrid();
        CurrentGrid = transform.GetComponent<NumGrid>();
        UnselectGridAtSrtart();
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

                    break;

                case ItemDir.Down:
                    CurrentGrid.MoveGridVertical(ItemDir.Down);
                    exportedThisMovement = false;

                    break;

                case ItemDir.Left:
                    //print("moving left" + InpController.tempDir);
                    CurrentGrid.MoveGridHorizontal(ItemDir.Left);
                    exportedThisMovement = false;

                    break;

                case ItemDir.Right:
                    CurrentGrid.MoveGridHorizontal(ItemDir.Right);
                    exportedThisMovement = false;

                    break;

                case ItemDir.None: break;
            }
        }

        if (movingGrid)
        {
            MoveAndScaleGrid();
        }
    }

    private void Initialize()
    {
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
        turretActiveButton.SetActive(true);
        startPos = transform.position;
        targetPos = turretChosen.transform.GetChild(1).transform.position;

        startScale = transform.localScale;
        TargetScale = new Vector2(0.2f, 0.2f);
    }

    public void UnselectGridAtSrtart()
    {
        t = 0;
        //move grid over to turret and change its scale
        movingGrid = true;
        MainHolder.AnimationOccuring = true;
        MainHolder.GridFocused = 0;

        startPos = transform.position;
        targetPos = turretChosen.transform.GetChild(1).transform.position;
        // print(targetPos.x + " " + targetPos.y);
        startScale = transform.localScale;
        TargetScale = new Vector2(0.2f, 0.2f);
        turretActiveButton.SetActive(true);

        //transform.position = targetPos;

        //transform.localScale = TargetScale;
    }

    public void SelectGrid()
    {
        t = 0;
        movingGrid = true;
        MainHolder.AnimationOccuring = true;
        //MainHolder.GridFocused = 0;

        startPos = transform.position;
        targetPos = centreGridSpot.transform.position;

        startScale = transform.localScale;
        TargetScale = new Vector2(1, 1);
    }

    private void MoveAndScaleGrid()//called in update, should move grid holder towards turret until close then set bool to false
    {
        t += Time.deltaTime / movementTimer;

        transform.position = Vector2.Lerp(startPos, targetPos, t);

        transform.localScale = Vector2.Lerp(startScale, TargetScale, t);

        MainHolder.AnimationOccuring = false;

        if (Vector2.Distance(transform.position, turretChosen.transform.GetChild(1).transform.position) > 0.1f)
        {
            //continue
        }
        else
        {
            movingGrid = false;
            t = 0;
            //cancel
        }
    }

    private void SpawnNewNumber(int numberValue = 2)// add a new number to the grid
    {
        int randRow, randCol;
        bool FoundEmptySpot = true;//set to false
                                   //pass go to gridspot to change sprite shown
                                   //get random spot on grid, check if it is empty

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

        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                // GameObject newTile =  Instantiate(GridSpot,new Vector2(col * GridSpacing,row * GridSpacing), Quaternion.identity);
                GameObject newTile = Instantiate(GridSpot, new Vector2(col, row), Quaternion.identity);
                newTile.transform.parent = GOHolder.transform;
                newTile.transform.GetComponent<GridSpot>().Initialise(0, col, row, CurrentGrid);

                // print("added item" + i + j);
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
    }

    private void ResetGridTile(GridSpot spot)
    {
        //takes spot and resets it

        spot.ResetSpotToDefaults();
    }
}