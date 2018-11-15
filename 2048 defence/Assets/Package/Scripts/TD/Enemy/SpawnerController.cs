using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {



    [Header("EnemyGameobjects")]
    public MinionController minionController;
    public GameObject enemyGOHolder;
    public GameObject enemy1;


    [Header("SpawnerOptions")]
    public int amountToSpawn = 1;
    [SerializeField]
    private int amountSpawned = 0;

	// Use this for initialization
	void Start () {

        InvokeRepeating("SpawnEnemy", 3, 3);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnEnemy()
    {

        if(amountSpawned < amountToSpawn)
        {
            //spawn enemy if amount spawned is less than amount to spawn

          GameObject tempGO =  Instantiate(enemy1, transform.position, transform.rotation);
            MinionDetails tempClassOfInst = tempGO.GetComponent<MinionDetails>();

            tempGO.transform.parent = enemyGOHolder.transform;
            tempClassOfInst.parent = minionController;
            tempClassOfInst.tileMap = tempClassOfInst.parent.PathwayTilemap;

            tempClassOfInst.parent.Minions.Add(tempGO);

            amountSpawned++;
        }


    }

}
