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
  //  public GameObject enemy1;
   

    [Header("Wave Options")]
    public List<GameObject> CurrentMinions = new List<GameObject>();
    public List<WaveDetails> waveDetailsList = new List<WaveDetails>();

    public Component[] components;
   // public WaveDetails[] waveDetailsList;

    private int waveMaxCount = 3;
    public int currentWave = 0;

    public float timeUntilWavesStart = 2f;
    public float timeBetweenWaves = 8f;

   // public int minionSpawnCount = 0;
   // private int minionSpawnMax = 5;




    [Header("SpawnerOptions")]
    public int amountToSpawn = 1;//TODO redudndent change to above
   // [SerializeField]
   // private int amountSpawned = 0;//TODO redudndent change to above
   // public float repeatDelay = 3f;//TODO redudndent change to above
    //public float repeatRepeatRate = 3f;//TODO redudndent change to above

    // Use this for initialization
    void Start () {

        InvokeRepeating("StartWave", timeUntilWavesStart, timeBetweenWaves);

        pathway = pathWayHolder.GetComponentsInChildren<Node>();

        components = GetComponents(typeof(WaveDetails));

        foreach(WaveDetails waveDets in components)
        {
            waveDetailsList.Add(waveDets);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
        


	}
    void StartWave()
    {
        CancelInvoke("SpawnEnemy");

        WaveDetails currWaveDetails = waveDetailsList[currentWave];
        currentWave++;

        float waveStartDelay = currWaveDetails.waveStartDelay;
        float spawnMinionDelay = currWaveDetails.waveStartDelay;
       // float minionSpawnMax = currWaveDetails.waveStartDelay;

         InvokeRepeating("SpawnEnemy",waveStartDelay,spawnMinionDelay);

    }
    void SpawnEnemy()
    {

        //spawn enemy if amount spawned is less than amount to spawn

        GameObject tempGO = Instantiate(Minions[0], transform.position, transform.rotation);
        MinionDetails tempClassOfInst = tempGO.GetComponent<MinionDetails>();
        //ustempGO.name = tempGO.name + minionSpawnCount;
        tempGO.transform.parent = enemyGOHolder.transform;
        tempClassOfInst.parent = minionController;
        // tempClassOfInst.tileMap = tempClassOfInst.parent.PathwayTilemap;
        tempClassOfInst.spawner = this;
        tempClassOfInst.NextPosition = pathway[tempClassOfInst.CurrentNode + 1].gameObject.transform.position;
        CurrentMinions.Add(tempGO);
        tempClassOfInst.healthCurrent = 6f;
       // minionSpawnCount++;


    }
    //GameObject GetRandomEnemy()
    //{
    //    int len = Minions.Count;
    //    int rand = Random.Range(0, len);
    //    GameObject temp = Minions[rand];
    //    return temp;
    //}
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
