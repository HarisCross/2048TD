﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInitManager : MonoBehaviour
{
    [Header("Turret")]
    // public List<GameObject> turretsGOList = new List<GameObject>();
    public List<Vector3> turretsPosList = new List<Vector3>();

    public GameObject turretHolder;
    public GameObject BulletHolder;

    [Header("Spawner")]
    public List<int> spawnersNumberTospawnList = new List<int>();

  //  public List<Vector3> SpawnerPosList = new List<Vector3>();

    public GameObject enemyGOHolder;
    public GameObject spawnerHolder;

    [Header("EndOfMap/Pathways")]
    public List<GameObject> pathwaysList = new List<GameObject>();
    public GameObject pathwayParentHolder;
    public GameObject endOfMap;

    [Header("2048Grid")]
    public List<GameObject> gridsGOList = new List<GameObject>();

    public List<Vector3> gridPosList = new List<Vector3>();
    public GameObject gridsHolder;

    [Header("VarsToBeUsedToAssign")]
    private GameObject gridsButtons;

    private GameObject centrePos;
    private GameObject GridHolder;
    private LevelManager levelManager;
    private GameObject gridUIButtons;

    private GameObject crosshairsHolder;
    // rivate GameObject textUIForTurretHolder;
    // public List<GameObject> crosshairs = new List<GameObject>();
    //public List<GameObject> textUIForTurrets = new List<GameObject>();

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void Awake()
    {
        InitialiseAndAssign();
    }

    private void InitialiseAndAssign()
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
        SpawnSpawner();
        SpawnEndOfMap();
    }

    private void FindVariablesToBeUsed()
    {
        gridsButtons = GameObject.Find("GridSelectionButtons");
        centrePos = GameObject.Find("CentreGridPos");
        levelManager = GameObject.Find("LevelManager").transform.GetComponent<LevelManager>();
        crosshairsHolder = GameObject.Find("Crosshairs");
        GridHolder = GameObject.Find("GridHolder");
        gridUIButtons = GameObject.Find("GridButton");//parent of the 3 grid ui buttons
            
        foreach(Transform child in pathwayParentHolder.transform)
        {

            pathwaysList.Add(child.gameObject);

        }
        pathwaysList.Remove(pathwayParentHolder.transform.GetChild(0).gameObject);
        
       // textUIForTurretHolder = GameObject.Find("TurretValueUI");
    }

    private void SpawnTurrets()
    {
        //for each turret pos spawn a turret at that pos and assign it the variables it needs
        //then spawn the ui needed for it - crosshair, shot counter and button to actiavte its grid
        print("turrets spawned");

        foreach (Vector3 loc in turretsPosList)
        {
            GameObject tempTurret = Instantiate(Resources.Load("Turrets/TurretV1") as GameObject);

            tempTurret.transform.position = loc;

            tempTurret.transform.parent = turretHolder.transform;
            tempTurret.GetComponent<TowerController>().bulletHolder = BulletHolder;

            //crosshairs

            GameObject tempCrosshairs = Instantiate(Resources.Load("Turrets/TargetCrosshair") as GameObject);

            tempCrosshairs.transform.parent = crosshairsHolder.transform;

            tempTurret.GetComponent<TowerController>().targetCrosshairs = tempCrosshairs;

            SpawnGameGrid(tempTurret);
        }
    }

    private void SpawnGameGrid(GameObject turret)
    {
        //for each turret spawn a game grid
        //create button to access the grid and link turret to grid and button
        //assign the grid to the grid controller
        print("gamegrid spawned");
        int counter = 0;
        foreach (Vector3 loc in gridPosList)
        {
            GameObject tempGrid = Instantiate(Resources.Load("2048/GameGrid") as GameObject);

            tempGrid.transform.parent = GridHolder.transform;
            tempGrid.GetComponent<Manager>().MainHolder = GridHolder.GetComponent<MainHolderController>();
            tempGrid.GetComponent<Manager>().InpController = GridHolder.GetComponent<InputController>();
            tempGrid.GetComponent<Manager>().turretChosen = turret;
            tempGrid.GetComponent<Manager>().centreGridSpot = centrePos;

            tempGrid.transform.position = gridPosList[counter];

            //add listeners to button. ;link button to grid, position over turret grid pos,assign main con to button, pass grid number to button

            GameObject tempButton = Instantiate(Resources.Load("Turrets/OpenGridButton") as GameObject);
            tempButton.transform.parent = gridsButtons.transform;
            Vector3 newScale = new Vector3(1, 1, 1);
            tempButton.transform.localScale = newScale;
            tempButton.GetComponent<GridButton>().mainController = GridHolder.GetComponent<MainHolderController>();
            tempButton.GetComponent<GridButton>().thisGrid = (counter + 1);

            tempButton.transform.position = turret.transform.GetChild(1).transform.position;
            tempButton.GetComponent<Button>().onClick.AddListener(tempGrid.GetComponent<Manager>().SelectGrid);

            tempGrid.GetComponent<Manager>().turretActiveButton = tempButton;
            GridHolder.GetComponent<UIButtonController>().AddGridToList(tempGrid);

            //set up the 3 grid ui buttons,add listener to grid select button

            gridUIButtons.transform.GetChild(0).transform.GetComponent<Button>().onClick.AddListener(gridsHolder.GetComponent<UIButtonController>().CloseGridButton);
            gridUIButtons.transform.GetChild(1).transform.GetComponent<Button>().onClick.AddListener(gridsHolder.GetComponent<UIButtonController>().ResetGridButton);
            gridUIButtons.transform.GetChild(2).transform.GetComponent<Button>().onClick.AddListener(gridsHolder.GetComponent<UIButtonController>().ExportGridButton);

            counter++;
        }
    }

    private void SpawnSpawner()
    {
        //foreach spawner in list spawn it at corresponding pos in poos list
       

        Object[] prefabList;
        prefabList = Resources.LoadAll("Spawners", typeof(GameObject));///get list of all prefabs in spawner folder

        foreach (int num in spawnersNumberTospawnList)
        {
            int pathwayChosen = Random.Range(0, pathwaysList.Count);
            GameObject pathwayChosenGO = pathwaysList[pathwayChosen].gameObject;

            print("spawner spawned");
            print(prefabList[num].name + " to be spawned");
            GameObject tempSpawner = Instantiate(prefabList[num], pathwayChosenGO.transform.GetChild(0).gameObject.transform.position, Quaternion.identity) as GameObject;//use the number given to get that from list made above and spawn using corrosponding vc3 from list

            //assign pathholder, enemy go holder, level manager

            tempSpawner.GetComponent<SpawnerController>().levelManager = levelManager;
            tempSpawner.GetComponent<SpawnerController>().pathWayHolder = pathwayParentHolder;
            tempSpawner.GetComponent<SpawnerController>().enemyGOHolder = enemyGOHolder;
            tempSpawner.transform.parent = spawnerHolder.transform;
            print("spawner spawned");
            //assign random pathway to spawner, move spawner pos to first child of pathway


            tempSpawner.GetComponent<SpawnerController>().pathWayHolder = pathwayChosenGO;
          //  tempSpawner.transform.position = ;//move spawner to spawner pos of that pathway


            pathwaysList.Remove(pathwayChosenGO);
        }
    }

    private void SpawnEndOfMap()
    {
        //mvoe the end of map at the final point in node chain
        print("endofmap spawned");
        GameObject parent;
        parent = endOfMap.transform.parent.transform.gameObject;

        GameObject pathway;
        pathway = parent.transform.GetChild((parent.transform.childCount - 1)).transform.gameObject;
        // print(parent.name+  pathway.name);

        GameObject lastChild;
        lastChild = pathway.transform.GetChild((pathway.transform.childCount - 1)).transform.gameObject;
        endOfMap.transform.position = lastChild.transform.position;
    }
}