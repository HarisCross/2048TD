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
    [SerializeField]
    private int wavesCompleted = 0;
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
        levelManager.amountOfSpawners++;
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
    public void TriggerEndLevelBGSplash(GameObject min)
    {

        levelManager.TriggerEndLevelBGSplash(min);

    }
    public void TriggerEndLevelStartOfAnim()
    {
        levelManager.TriggerEndLevelStartOfAnim();

    }
    public void CheckIfAllWavesDoneAndMinsDead()
    {
        //called by wave details when all of its minions are dead
        wavesCompleted+=1;

        if(wavesCompleted == waveMaxCount)
        {
            print("All waves completed for: " + this.transform.gameObject.name);
            levelManager.counterSpawnersCompleted++;
            levelManager.LevelCompeletion();
        }

    }
    public bool CheckIfLastWave(int waveNum)
    {

        if(waveNum + 1 == waveDetailsList.Count)
        {
           // print("DETECTED ITS ON LAST WAVE");
            return true;
        }
        else
        {
            //print("DETECTED ITS NOT!! ON THE LAST WAVE");
            return false;
        }
    }

    public void CheckIfLastMinionAcrossAllWaves()
    {
        //called when 1 minion left in a wave
        //should check all waves and check if any of those have any left, if so then do nothing else then its last alive and trigger its value on mindetails

        bool isLastAlive = true;
        List<GameObject> listOfminionsLeft = new List<GameObject>();

        foreach (WaveDetails waveDets in components)
        {

            //if(waveDets.CurrentMinionsInPlay.Count != 0)//if there is something alive else do nothing and check next waveDets
            //{
            //    if (waveDets.CurrentMinionsInPlay.Count == 1)//if 1 alivue, compare to min given.
            //    {
            //        if (waveDets.CurrentMinionsInPlay[0].gameObject == lastMinion)// if there is 1 in list and its same as one being checked
            //        {



            //        }
            //        else
            //        {
            //            isLastAlive = false;
            //        }
            //    }
            //    else
            //    {
            //        //more than 1 minion in list so not last alive
            //        isLastAlive = false;

            //    }

            //}
            //else
            //{
            //    //the wave checked is empty
            //    isLastAlive = true;


            //}
            listOfminionsLeft.AddRange(waveDets.CurrentMinionsInPlay);

        }

        if(listOfminionsLeft.Count == 1)
        {
            //only 1 min alive
            listOfminionsLeft[0].GetComponent<MinionDetails>().lastMinionAlive = true;
        }

    }
    private void StartWave()
    {
        levelManager.AcitvateLevelTimer();
        if (currentWave == waveMaxCount)
        {
            CancelInvoke("StartWave");
          //  print("finished waves");
            //this spawner has finished all the waves assigned to it
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
        currWaveDetails.minionsSpawned++;

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

        //if done last node on pathway then change target to second node in array

        MinionDetails tempMinDetails = minion.GetComponent<MinionDetails>();

        tempMinDetails.StartPosition = minion.transform.position;
        if(tempMinDetails.CurrentNode + 1== pathway.Length)//when on last node change current to start to allow enemies to continue around
        {

            tempMinDetails.CurrentNode = 0;

        }
      //  print(tempMinDetails.CurrentNode + " : " + pathway.Length);
        tempMinDetails.NextPosition = pathway[tempMinDetails.CurrentNode + 1].gameObject.transform.position;
        tempMinDetails.CurrentNode++;


        tempMinDetails.timer = 0f;

        // }
    }
}