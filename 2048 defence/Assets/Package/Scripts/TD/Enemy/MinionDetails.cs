using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinionDetails : MonoBehaviour {



    public int health = 0;



    public MinionController parent;


    public Tilemap tileMap;
    private Vector3Int targetTilePos, currentTilePos;
   // private TileBase targetTile, currentTile;
    private Vector3 targetPos, currentPos;

    Vector3Int cellPosition;
    int gridX, gridY,gridZ;

    void Start()
    {
        cellPosition = tileMap.WorldToCell(transform.position);

        gridX = cellPosition[0];
        gridY = cellPosition[1];
        gridZ = cellPosition[2];
        currentTilePos = cellPosition;
        //print("X: " + gridX + "   Y: " + gridY + "   Z: " + gridZ);

        GetNextPosition();
    } 
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnDestroy()
    {
        parent.Minions.Remove(transform.gameObject);
    }
    public void StartMovement()
    {

        //  targetTile = tileMap.GetTile(targetTilePos);
        //  currentTile = tileMap.GetTile(currentTilePos);

        // targetPos = targetTile.

        targetPos = targetTilePos;
        currentPos = currentTilePos;

        //print("from: " + currentPos + " to " + targetPos);

    }

    private void GetNextPosition()
    {

        for(int x = gridX - 1; x <= gridX + 1; x++)
        {
            for (int y = gridY - 1; y <= gridY + 1; y++)
            {

                Vector3Int  tempVec3Int = new Vector3Int(x, y, gridZ);

                if (tempVec3Int != cellPosition)
                {


                    if (tileMap.GetSprite(tempVec3Int))
                    {
                        print("got sprite" + tempVec3Int);
                        //currentTilePos = cellPosition;
                        targetTilePos = tempVec3Int;

                    }
                    //print(tempVec3Int);
                }
            }


        }



    }


}
