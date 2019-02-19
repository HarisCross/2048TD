using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInitManager : MonoBehaviour
{
    //preset values to be used to position the buttons in the right spot and the right size depending on how many buttons are in uset that game
    private Vector3[] GridButtonPosArrayS3 = new[] { new Vector3(-62, -285, 0.08f), new Vector3(31, -285, 0.08f), new Vector3(130, -285, 0.08f) };
     private float GridButtonPosArrayS3Size = 1.5F;

    private Vector3[] GridButtonPosArrayS2 = new[] { new Vector3(-40, -285, 0.08f), new Vector3(106, -285, 0.08f) };
        private float GridButtonPosArrayS2Size = 2.2F;

    private Vector3[] GridButtonPosArrayS1 = new[] { new Vector3(34, -285, 0.08f) };
     private float GridButtonPosArrayS1Size = 4.5F;

    float newScaleSizeForButton;
    Vector3[] newPosForButton;
    int newPosForButtonCounter = 0;
    int counter = 0;

    [Header("Turret")]
    // public List<GameObject> turretsGOList = new List<GameObject>();
    public List<Vector3> turretsPosList = new List<Vector3>();
    public List<GameObject> turretsGOList = new List<GameObject>();

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
 //   private MainHolderController mainHolderController;
    public List<Vector3> gridPosList = new List<Vector3>();
    public GameObject gridsHolder;

    [Header("VarsToBeUsedToAssign")]
    private GameObject gridsButtons;

    private GameObject spawningGridPos;
    private GameObject CentreGridPos;
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
        spawningGridPos = GameObject.Find("SpawningGridPos");
        CentreGridPos = GameObject.Find("CentreGridPos");
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
       // print("turrets spawned");

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

            turretsGOList.Add(tempTurret);
        }

        int buttonsToSpawn = turretsGOList.Count;
        //int buttonsToSpawnCounter = 0;
        switch (buttonsToSpawn)
        {
            case 1:
                newScaleSizeForButton = GridButtonPosArrayS1Size;
                newPosForButton = GridButtonPosArrayS1;
                break;
            case 2:
                newScaleSizeForButton = GridButtonPosArrayS2Size;
                newPosForButton = GridButtonPosArrayS2;

                break;
            case 3:
                newScaleSizeForButton = GridButtonPosArrayS3Size;
                newPosForButton = GridButtonPosArrayS1;

                break;
            default: print("this really shouldnt ever get here"); break;
        }

        foreach (GameObject turr in turretsGOList)
        {
            SpawnGameGrid(turr);
        }
    }

    private void SpawnGameGrid(GameObject turret)
    {

        AddVarsToGridHolder();
        //for each turret spawn a game grid
        //create button to access the grid and link turret to grid and button
        //assign the grid to the grid controller
        //print("gamegrid spawned");
        

        //foreach(Vector3 newPos in newPosForButton)
        //{
        //    print("new: " +newPos);
        //}
      //  foreach (Vector3 loc in gridPosList)
     //   {
           // GameObject tempGrid = Instantiate(Resources.Load("2048/GameGrid") as GameObject);
            GameObject tempGrid = Instantiate(Resources.Load("2048/GameGridTest") as GameObject);

            tempGrid.transform.name = tempGrid.transform.name +" : " +counter;
            tempGrid.transform.parent = GridHolder.transform;
            tempGrid.GetComponent<Manager>().MainHolder = GridHolder.GetComponent<MainHolderController>();
            tempGrid.GetComponent<Manager>().InpController = GridHolder.GetComponent<InputController>();
            tempGrid.GetComponent<Manager>().turretChosen = turret;
            tempGrid.GetComponent<Manager>().BoardNumber = (counter+1);
            tempGrid.GetComponent<Manager>().centreGridPos = CentreGridPos;
            tempGrid.GetComponent<Manager>().spawningGridPos = spawningGridPos;
            tempGrid.GetComponent<Manager>().BoardNumber = (counter+1);
           // print("assigned board number: " + (counter + 1));
        GridHolder.GetComponent<MainHolderController>().GridsList.Add(tempGrid);
              tempGrid.transform.position = spawningGridPos.transform.position ;

            //add listeners to button. ;link button to grid, position over turret grid pos,assign main con to button, pass grid number to button

            GameObject tempButton = Instantiate(Resources.Load("Turrets/OpenGridButton") as GameObject);
            tempButton.transform.parent = gridsButtons.transform;
            tempButton.transform.GetChild(0).transform.GetComponent<Text>().text = "Open Grid: " + newPosForButtonCounter.ToString();
        // tempButton.transform.position = turret.transform.GetChild(1).transform.position;
        //  print(newPosForButton[counter]);
            tempButton.GetComponent<RectTransform>().localPosition = newPosForButton[newPosForButtonCounter];


            Vector3 newScale = new Vector3(newScaleSizeForButton, 1, 1);
            tempButton.transform.localScale = newScale;

            tempButton.GetComponent<GridButton>().mainController = GridHolder.GetComponent<MainHolderController>();
            tempButton.GetComponent<GridButton>().thisGrid = counter+1;

            //tempButton.GetComponent<Button>().onClick.AddListener(tempGrid.GetComponent<Manager>().SelectGrid);

            tempGrid.GetComponent<Manager>().turretActiveButton = tempButton;
            GridHolder.GetComponent<UIButtonController>().AddGridToList(tempGrid);

            //set up the 3 grid ui buttons,add listener to grid select button

            gridUIButtons.transform.GetChild(0).transform.GetComponent<Button>().onClick.AddListener(gridsHolder.GetComponent<UIButtonController>().CloseGridButton);
            gridUIButtons.transform.GetChild(1).transform.GetComponent<Button>().onClick.AddListener(gridsHolder.GetComponent<UIButtonController>().ResetGridButton);
            gridUIButtons.transform.GetChild(2).transform.GetComponent<Button>().onClick.AddListener(gridsHolder.GetComponent<UIButtonController>().ExportGridButton);
           // buttonsToSpawnCounter++;
            counter++;
            newPosForButtonCounter++;
      //  }


        //TODO::spawn button at correct position using array made, move game grid to below game screen, add function to raise and lower grid on button click,



    }
    private void AddVarsToGridHolder()
    {
        //add ui variables to the grid holder to be used and passed down
        GridHolder.GetComponent<MainHolderController>().CentrePos = spawningGridPos;
        GridHolder.GetComponent<MainHolderController>().interactButtonsHolder = gridUIButtons;

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

           // print("spawner spawned");
           // print(prefabList[num].name + " to be spawned");
            GameObject tempSpawner = Instantiate(prefabList[num], pathwayChosenGO.transform.GetChild(0).gameObject.transform.position, Quaternion.identity) as GameObject;//use the number given to get that from list made above and spawn using corrosponding vc3 from list

            //assign pathholder, enemy go holder, level manager

            tempSpawner.GetComponent<SpawnerController>().levelManager = levelManager;
            tempSpawner.GetComponent<SpawnerController>().pathWayHolder = pathwayParentHolder;
            tempSpawner.GetComponent<SpawnerController>().enemyGOHolder = enemyGOHolder;
            tempSpawner.transform.parent = spawnerHolder.transform;
          //  print("spawner spawned");
            //assign random pathway to spawner, move spawner pos to first child of pathway


            tempSpawner.GetComponent<SpawnerController>().pathWayHolder = pathwayChosenGO;
          //  tempSpawner.transform.position = ;//move spawner to spawner pos of that pathway


            pathwaysList.Remove(pathwayChosenGO);
        }
    }

    private void SpawnEndOfMap()
    {
        //mvoe the end of map at the final point in node chain
      //  print("endofmap spawned");
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