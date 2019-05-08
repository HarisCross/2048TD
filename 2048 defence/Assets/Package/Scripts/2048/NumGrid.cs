using UnityEngine;

public class NumGrid : MonoBehaviour
{
    // public int maxRowCol = 4;
    public GridSpot[,] BoardGrid = new GridSpot[4, 4];

    private Manager thisManager;
    private string tempGrid;

    private int[] directionSequenceLeft = new int[] { 1, 2, 3 };
    private int[] directionSequenceRight = new int[] { 2, 1, 0 };

    private GridSpot SpotToMoveToo, prevSearchSpot;
    private bool leftDownCol, rightUpCol;

    // Use this for initialization
    private void Start()
    {
        thisManager = transform.GetComponent<Manager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9)) ShowGridOnConsole();
    }

    private void ShowGridOnConsole()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                tempGrid += BoardGrid[i, j].NumberValue + "||";
            }
            tempGrid += "|||";
        }

        Debug.Log(tempGrid);
        tempGrid = null;
    }

    public void MoveGridVertical(ItemDir Direction)
    {
        //gets passed left or right
        print("moving grid vertical: " + Direction);
        int[] dir;

        if (Direction == ItemDir.Down)//sets order to do the cols in,
        {
            dir = directionSequenceLeft;//1,2,3
            leftDownCol = true;
            rightUpCol = false;
        }
        else
        {
            dir = directionSequenceRight;//2,1,0
            rightUpCol = true;
            leftDownCol = false;
        }

        foreach (int row in dir)//
        {
            for (int col = 0; col < 4; col++)//
            {
                GridSpot compareSpot, currentSpot;
                currentSpot = BoardGrid[col, row];
                if (currentSpot.NumberValue == 0) continue;

                if (leftDownCol)
                {
                    // print(currentSpot + " should check " + col);
                    print("Checking: " + currentSpot.name);
                    for (int u = row; u > 0; u--)
                    {
                        //print(currentSpot + " : " + BoardGrid[col, u - 1]);
                        compareSpot = BoardGrid[col, u - 1];
                        GridSpot tempValue;
                        tempValue = CompareTwoSpots(currentSpot, compareSpot);

                        if (tempValue != null)
                        {
                            currentSpot = tempValue;
                        }
                        else
                        {
                            continue;
                        }
                        tempValue = null;
                    }
                }


                if (rightUpCol)
                {
                    for (int u = row; u < 3; u++)
                    {
                        // print(currentSpot + " : " + BoardGrid[u + 1, row]);
                        compareSpot = BoardGrid[col, u + 1];

                        GridSpot tempValue;
                        tempValue = CompareTwoSpots(currentSpot, compareSpot);

                        if (tempValue != null)
                        {
                            currentSpot = tempValue;
                        }
                        else
                        {
                            continue;
                        }
                        tempValue = null;
                    }
                }
            }
        }
        ResetAllGridDuplications();
        thisManager.SpawnTwoNewNumbers();
    }

    public void MoveGridHorizontal(ItemDir Direction)
    {
        //gets passed left or right
        print("moving grid horizontal: " + Direction);
        int[] dir;

        if (Direction == ItemDir.Left)//sets order to do the cols in, mvoing grid down
        {
            dir = directionSequenceLeft;//1,2,3
            leftDownCol = true;
            rightUpCol = false;
        }
        else
        {
            dir = directionSequenceRight;//2,1,0
            rightUpCol = true;
            leftDownCol = false;
        }

        foreach (int col in dir)//
        {
            for (int row = 0; row < 4; row++)//
            {
                GridSpot compareSpot, currentSpot;
                currentSpot = BoardGrid[col, row];
                if (currentSpot.NumberValue == 0) continue;

                //doesnt need to compare to the left if there isnt anything to the left
                //check if the current tile has a value, if so then do below###
                //search value in direction of current spot, do below in loop for its coloum number so if its col 1 then search only 1 spot to left, if its 2 then search the two spots to its left

                // check its value if empty then move there
                //if not then compare numbers, if same
                //merge the two numbers by doubling that spots value, change its sprite and wiping the old spot
                //else
                // do nothing if the numbers cant merge and its not empt

                if (leftDownCol)
                {
                    for (int u = col; u > 0; u--)
                    {
                        compareSpot = BoardGrid[u - 1, row];

                        //go through loop going left, if nothing then move and update the current tile to allow additional movement
                        // if value then merge and update current to allow additional movement
                        //if cant merge then do nothing

                        //if its merged or moved then return current spot and update it then keep going left
                        GridSpot tempValue;
                        tempValue = CompareTwoSpots(currentSpot, compareSpot);

                        if (tempValue != null)
                        {
                            currentSpot = tempValue;
                        }
                        else
                        {
                            continue;
                        }
                        tempValue = null;
                    }
                }
                if (rightUpCol)
                {
                    for (int u = col; u < 3; u++)
                    {
                        //  print(currentSpot + " : " + BoardGrid[u + 1, row]);
                        compareSpot = BoardGrid[u + 1, row];

                        //go through loop going left, if nothing then move and update the current tile to allow additional movement
                        // if value then merge and update current to allow additional movement
                        //if cant merge then do nothing

                        //if its merged or moved then return current spot and update it then keep going left
                        GridSpot tempValue;
                        tempValue = CompareTwoSpots(currentSpot, compareSpot);

                        if (tempValue != null)
                        {
                            currentSpot = tempValue;
                        }
                        else
                        {
                            continue;
                        }
                        tempValue = null;
                    }
                }
            }
        }
        ResetAllGridDuplications();
        thisManager.SpawnTwoNewNumbers();
    }

    private GridSpot CompareTwoSpots(GridSpot curr, GridSpot spotToComp)
    {
        if (spotToComp.NumberValue == 0)//if new spot is empty,should return new spot
        {
            MoveItemToNewSpotEmpty(curr, spotToComp);

            return spotToComp;//should return the updated current spot for futher checks
        }
        if (spotToComp.NumberValue == curr.NumberValue && curr.hasBeenMerged == false && spotToComp.hasBeenMerged == false)//if spot is same,should return new spot
        {
            MoveItemToNewSpotMerge(curr, spotToComp);

            return spotToComp;//should return the updated current spot for futher checks
        }
        else//do nothing
        {
        }
        return null;
    }

    private void MoveItemToNewSpotMerge(GridSpot currSpot, GridSpot NewSpot)//call to merge two spots togehter
    {
        int newValue = currSpot.NumberValue * 2;
        GameObject newSpri = thisManager.GetSuitableSprite(newValue);

        NewSpot.ModifyNumber(newSpri, newValue);
        NewSpot.hasBeenMerged = true;
        currSpot.ResetSpotToDefaults();
    }

    private void MoveItemToNewSpotEmpty(GridSpot currSpot, GridSpot NewSpot)//call to move spot over to empty spot
    {
        GameObject newSpri = thisManager.GetSuitableSprite(currSpot.NumberValue);

        NewSpot.ModifyNumber(newSpri, currSpot.NumberValue);
        if (currSpot.hasBeenMerged) { NewSpot.hasBeenMerged = true; }
        currSpot.ResetSpotToDefaults();
    }

    private void ResetAllGridDuplications()
    {
        //called at end of all movements to reset the tiles bool to allow for swapping again next movement

        foreach (GridSpot spot in BoardGrid)
        {
            spot.ResetSwapStatus();
        }
    }
}