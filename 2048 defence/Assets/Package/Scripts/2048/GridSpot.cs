using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpot : MonoBehaviour {

    public float GridSpacing = 1f;

    public GameObject DefaultGO;
    public GameObject SpriteToShow;


    [Header("Grid Spot Values")]
    public int NumberValue;
    public int RowLoc;
    public int ColLoc;
    public NumGrid CurrentGrid;
    public bool hasBeenMerged = false; // once number has been changed swap to true to avoid multiple swaps in one turn

    public void ChangeGOSprite(GameObject newGO)
    {
        SpriteToShow = newGO;
    }

    public void ModifyNumber(GameObject newSprite,int newNumber)
    {

        DefaultGO.SetActive(false);
        Destroy(SpriteToShow);
        SpriteToShow = Instantiate(newSprite, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        SpriteToShow.transform.parent = transform;
        NumberValue = newNumber;

    }

    public void Initialise(int num, int col, int row,NumGrid Grid)
    {
        NumberValue = num;
        RowLoc = row;
        ColLoc = col;
        CurrentGrid = Grid;
        transform.name = "GridSpot" + ColLoc + RowLoc;
        CurrentGrid.BoardGrid[col, row] = transform.gameObject.GetComponent<GridSpot>();
        //ChangeGOSprite(tile);
        DefaultGO = Instantiate(DefaultGO, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        DefaultGO.transform.parent = transform;
    }

    public void ResetSwapStatus()
    {
        hasBeenMerged = false;
    }
    public void ResetSpotToDefaults()
    {
        hasBeenMerged = false;

        // ModifyNumber(DefaultGO, 0);
        DefaultGO.SetActive(true);
        Destroy(SpriteToShow);
        NumberValue = 0;

    }
}
