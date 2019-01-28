using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitManager : MonoBehaviour {

    [Header("Turret")]
   // public List<GameObject> turretsGOList = new List<GameObject>();
    public List<Vector3> turretsPosList = new List<Vector3>();
    public GameObject turretHolder;
    public GameObject BulletHolder;

    [Header("Spawner")]
    public List<GameObject> spawnersGOList = new List<GameObject>();
    public List<Vector3> SpawnerPosList = new List<Vector3>();

    [Header("EndOfMap")]
    public GameObject endOfMap;

    [Header("2048Grid")]
    public List<GameObject> gridsGOList = new List<GameObject>();

    [Header("VarsToBeUsedToAssign")]
    private GameObject gridsButtons;
    private GameObject centrePos;
    private LevelManager levelManager;

    private GameObject crosshairsHolder;
    private GameObject textUIForTurretHolder;
   // public List<GameObject> crosshairs = new List<GameObject>();
    //public List<GameObject> textUIForTurrets = new List<GameObject>();



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void Awake()
    {
        InitialiseAndAssign();
    }
    void InitialiseAndAssign()
    {
        //should run through the lists above and assign variables that they need
        //foreach tower in lsit spawn a crosshairs, move it to the holder 
        //needs to spawn crosshairs and text ui counter for each

        //    assign text to turret
        //assign buttons to main grid holder
        //assign crosshairs to tower
        //assign centre pos to main grid holder
        //assign level manager to each spawner
        FindVariablesToBeUsed();




        SpawnTurrets();
        SpawnGameGrid();
        SpawnSpawner();
        SpawnEndOfMap();
    }
    void FindVariablesToBeUsed()
    {
        gridsButtons = GameObject.Find("GridButton");
        centrePos = GameObject.Find("CentreGridPos");
        levelManager = GameObject.Find("LevelManager").transform.GetComponent<LevelManager>();
        crosshairsHolder = GameObject.Find("Crosshairs");
        textUIForTurretHolder = GameObject.Find("TurretValueUI");



    }
    void SpawnTurrets()
    {
        //for each turret pos spawn a turret at that pos and assign it the variables it needs
        //then spawn the ui needed for it - crosshair, shot counter and button to actiavte its grid
        print("turrets spawned");

        foreach(Vector3 loc in turretsPosList)
        {
            GameObject tempTurret = Instantiate(Resources.Load("Turrets/TurretV1") as GameObject);

            tempTurret.transform.position = loc;

            tempTurret.transform.parent = turretHolder.transform;
            tempTurret.GetComponent<TowerController>().bulletHolder = BulletHolder;

            //crosshairs, text ui for shot

            GameObject tempCrosshairs = Instantiate(Resources.Load("Turrets/TargetCrosshair") as GameObject);

            tempCrosshairs.transform.parent = crosshairsHolder.transform;

            tempTurret.GetComponent<TowerController>().targetCrosshairs = tempCrosshairs;






        }




    }
    void SpawnGameGrid()
    {
        //for each turret spawn a game grid
        print("gamegrid spawned");


    }
    void SpawnSpawner()
    {
        //foreach spawner in list spawn it at corresponding pos in poos list
        print("spawner spawned");


    }
    void SpawnEndOfMap()
    {
        //mvoe the end of map at the final point in node chain
        print("endofmap spawned");
        GameObject parent;
        parent = endOfMap.transform.parent.transform.gameObject;

        GameObject pathway;
        pathway = parent.transform.GetChild((parent.transform.childCount-1)).transform.gameObject;
        // print(parent.name+  pathway.name);

        GameObject lastChild;
        lastChild = pathway.transform.GetChild((pathway.transform.childCount - 1)).transform.gameObject;
        endOfMap.transform.position = lastChild.transform.position;
    }
}
