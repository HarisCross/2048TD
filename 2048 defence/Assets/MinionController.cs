using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinionController : MonoBehaviour {

    public Tilemap PathwayTilemap;

    public List<GameObject> Minions = new List<GameObject>();


	// Use this for initialization
	void Start () {

        InvokeRepeating("MoveMinions", 5f, 1f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void MoveMinions()
    {

        foreach(GameObject min in Minions)
        {

            min.GetComponent<MinionDetails>().StartMovement();
           


        }

    }
}
