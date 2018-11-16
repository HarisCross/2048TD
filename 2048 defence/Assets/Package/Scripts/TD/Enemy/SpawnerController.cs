using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

    [Header("Pathing")]
    public GameObject pathWayHolder;//holds the GO that contains the pathway for this spawners route
    Node[] pathway;

    [Header("EnemyGameobjects")]
    public MinionController minionController;
    public GameObject enemyGOHolder;
    public GameObject enemy1;
    public List<GameObject> Minions = new List<GameObject>();


    [Header("SpawnerOptions")]
    public int amountToSpawn = 1;
    [SerializeField]
    private int amountSpawned = 0;
    public float repeatDelay = 3f;
    public float repeatRepeatRate = 3f;

    // Use this for initialization
    void Start () {

        InvokeRepeating("SpawnEnemy", repeatDelay, repeatRepeatRate);

        pathway = pathWayHolder.GetComponentsInChildren<Node>();

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
            tempGO.name = tempGO.name + amountSpawned;
            tempGO.transform.parent = enemyGOHolder.transform;
            tempClassOfInst.parent = minionController;
            // tempClassOfInst.tileMap = tempClassOfInst.parent.PathwayTilemap;
            tempClassOfInst.spawner = this;
            tempClassOfInst.NextPosition = pathway[tempClassOfInst.CurrentNode + 1].gameObject.transform.position;
            Minions.Add(tempGO);

            amountSpawned++;
        }
     

    }

   public void UpdateMinionTarget(GameObject minion)
    {
        //itterate through all minions and update thier target pos to the next one in array

        
       // foreach(GameObject minion in Minions)
        //{
            MinionDetails tempMinDetails = minion.GetComponent<MinionDetails>();

            tempMinDetails.StartPosition = minion.transform.position;
            tempMinDetails.NextPosition = pathway[tempMinDetails.CurrentNode + 1].gameObject.transform.position;
            tempMinDetails.CurrentNode++;
            tempMinDetails.timer = 0f;
            

       // }

    }
}
