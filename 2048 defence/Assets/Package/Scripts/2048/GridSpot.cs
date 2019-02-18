using UnityEngine;

public class GridSpot : MonoBehaviour
{
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

    public void ModifyNumber(GameObject newSprite, int newNumber)
    {
        DefaultGO.SetActive(false);
        Destroy(SpriteToShow);
        SpriteToShow = Instantiate(newSprite, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        SpriteToShow.transform.parent = transform;
        //SpriteToShow.transform.localScale.x = 1f;
        Vector3 newScale = new Vector3(1, 1, 1);
        SpriteToShow.transform.localScale = newScale;
        NumberValue = newNumber;
    }

    public void Initialise(int num, int col, int row, NumGrid Grid)
    {
        NumberValue = num;
        RowLoc = row;
        ColLoc = col;
        CurrentGrid = Grid;
        transform.name = "GridSpot" + ColLoc + RowLoc;
        CurrentGrid.BoardGrid[col, row] = transform.gameObject.GetComponent<GridSpot>();
        //ChangeGOSprite(tile);
      //  print("making grid spot at " + col + " : " + row);

        DefaultGO = Instantiate(DefaultGO, new Vector3(transform.position.x, transform.position.y,1), Quaternion.identity);
        //DefaultGO = Instantiate(DefaultGO, new Vector3(col, row,1), Quaternion.identity);
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