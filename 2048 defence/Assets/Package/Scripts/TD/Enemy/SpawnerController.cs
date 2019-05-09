using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Pathing")]
    public GameObject pathWayHolder;//holds the GO that contains the pathway for this spawners route

    private Node[] pathway;

    [Header("EnemyGameobjects")]
    public MinionController minionController;

    public GameObject enemyGOHolder;
    //  public GameObject enemy1;

    [Header("Wave Options")]
    // public List<GameObject> CurrentMinions = new List<GameObject>();
    public List<WaveDetails> waveDetailsList = new List<WaveDetails>();

    public WaveDetails currWaveDetails;
    private Component[] components;

    // [SerializeField]
    public int waveMinionCounter = 0;
    public int LevelMinionCounter = 0;

    // public WaveDetails[] waveDetailsList;

    public int waveMaxCount = 2;//used to end level, its how many waves there should be
    public int currentWave = 0;
    private int minionToReturnNumber;
    public float timeUntilWavesStart = 2f;//delay from start until first wave starts
    public float timeBetweenWaves = 10f;//the time between waves of enemys

    // public int minionSpawnCount = 0;
    // private int minionSpawnMax = 5;

    public LevelManager levelManager;

    [Header("SpawnerOptions")]
   // public int amountToSpawn = 1;//TODO redudndent change to above
    public bool tutorialSpawner = false;
    // [SerializeField]
    // private int amountSpawned = 0;//TODO redudndent change to above
    // public float repeatDelay = 3f;//TODO redudndent change to above
    //public float repeatRepeatRate = 3f;//TODO redudndent change to above

    // Use this for initialization
    private void Start()
    {
        pathway = pathWayHolder.GetComponentsInChildren<Node>();

        components = GetComponents(typeof(WaveDetails));

        foreach (WaveDetails waveDets in components)
        {
            waveDetailsList.Add(waveDets);
            waveDets.spawnContrller = this;
        }

        if (tutorialSpawner)
        {
            //infinite wave spawner
            InvokeRepeating("StartInfiniteWaves", timeUntilWavesStart, timeBetweenWaves);


        }
        else
        {
            InvokeRepeating("StartWave", timeUntilWavesStart, timeBetweenWaves);

        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void StartWave()
    {
        levelManager.AcitvateLevelTimer();
        if (currentWave == waveMaxCount)
        {
            CancelInvoke("StartWave");
            print("finished waves");
            return;
        }
        //  print("starting wave: " + currentWave);
        if (!(currentWave == 0))
        {
            levelManager.WaveComplete(this, currentWave);
        }
        CancelInvoke("SpawnEnemy");

        currWaveDetails = waveDetailsList[currentWave];
        currentWave++;
        waveMinionCounter = 0;
        

        float waveStartDelay = currWaveDetails.waveStartDelay;
        float spawnMinionDelay = currWaveDetails.spawnMinionDelay;
        // float minionSpawnMax = currWaveDetails.waveStartDelay;
        minionToReturnNumber = waveMinionCounter;
        InvokeRepeating("SpawnEnemy", waveStartDelay, spawnMinionDelay);
    }
    private void StartInfiniteWaves()
    {
        print("starting infinite waves");
        currWaveDetails = waveDetailsList[0];
        float waveStartDelay = currWaveDetails.waveStartDelay;
        float spawnMinionDelay = currWaveDetails.spawnMinionDelay;

        CancelInvoke("SpawnMinionInfiniteWave");

        currentWave++;
        waveMinionCounter = 0;

        // float minionSpawnMax = currWaveDetails.waveStartDelay;
        minionToReturnNumber = waveMinionCounter;
        InvokeRepeating("SpawnMinionInfiniteWave", waveStartDelay, spawnMinionDelay);


    }
    private GameObject GetMinionToSpawn()
    {
        //get a minion from the current wave minions, go sequentially to allow for specific waves of enemys.
        //repeat if max minions is more than the minion list

        GameObject ret;
        int minionListLength = currWaveDetails.Minions.Count;
        //minionToReturnNumber = waveMinionCounter;
        //print("length: " + minionListLength + ", return number: " + minionToReturnNumber + ", wave min counter: " + waveMinionCounter);

        if (minionToReturnNumber > (minionListLength - 1))//length may be 6 but has 0-5 within so minus 1 to prevent index error
        {
            //if minions couinter is larger then length reset it to 0
            minionToReturnNumber = 0;
        }

        ret = currWaveDetails.Minions[minionToReturnNumber];
        //print("returned: " + ret.name);
        return ret;
    }

    private void SpawnEnemy()
    {
        if (waveMinionCounter > currWaveDetails.waveMinionMax)
        {
            CancelInvoke("SpawnEnemy");

            waveMinionCounter = 0;
            return;
        }
        //spawn enemy if amount spawned is less than amount to spawn
        GameObject minion = GetMinionToSpawn();

        GameObject tempGO = Instantiate(minion, transform.position, transform.rotation);
        MinionDetails tempClassOfInst = tempGO.GetComponent<MinionDetails>();

        tempGO.transform.parent = enemyGOHolder.transform;
        tempClassOfInst.parent = minionController;
        tempClassOfInst.spawner = this;
        tempClassOfInst.NextPosition = pathway[tempClassOfInst.CurrentNode + 1].gameObject.transform.position;
        //CurrentMinions.Add(tempGO);

        currWaveDetails.CurrentMinionsInPlay.Add(tempGO);

        tempClassOfInst.healthCurrent = 6f;

        tempClassOfInst.moveSpeed = currWaveDetails.waveMinionSpeed;

        waveMinionCounter++;
        LevelMinionCounter = currWaveDetails.CurrentMinionsInPlay.Count;
        minionToReturnNumber++;
    }
    private void SpawnMinionInfiniteWave()
    {
        print("spawning infinties waves");
        if (waveMinionCounter > currWaveDetails.waveMinionMax)
        {
            CancelInvoke("SpawnMinionInfiniteWave");

           
            return;
        }
        //spawn enemy if amount spawned is less than amount to spawn
        GameObject minion = GetMinionToSpawn();

        GameObject tempGO = Instantiate(minion, transform.position, transform.rotation);
        MinionDetails tempClassOfInst = tempGO.GetComponent<MinionDetails>();

        tempGO.transform.parent = enemyGOHolder.transform;
        tempClassOfInst.parent = minionController;
        tempClassOfInst.spawner = this;
        tempClassOfInst.NextPosition = pathway[tempClassOfInst.CurrentNode + 1].gameObject.transform.position;
        //CurrentMinions.Add(tempGO);

        currWaveDetails.CurrentMinionsInPlay.Add(tempGO);

        //tempClassOfInst.healthCurrent = 6f;//TODO//change health to the correct value

        tempClassOfInst.moveSpeed = currWaveDetails.waveMinionSpeed;

        waveMinionCounter++;
        minionToReturnNumber++;

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