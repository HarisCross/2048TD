using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour
{
    public MainHolderController mainController;

    public List<GameObject> GameGrids = new List<GameObject>();

    [SerializeField]
    private float TimeBetweenExports = 5f;
    [SerializeField]
    private float TimeBetweenExportsTimer = 5f;
    [SerializeField]
    private bool gridExportsReady = true;

    //public Manager GameGrid1;
    //public Manager GameGrid2;
    //public Manager GameGrid3;
    // Use this for initialization
    private void Start()
    {
        mainController = transform.GetComponent<MainHolderController>();
        //GameGrid1.UnselectGrid();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void AddGridToList(GameObject newGrid)
    {
        GameGrids.Add(newGrid);
        //  newGrid.GetComponent<Manager>().UnselectGridAtSrtart();
    }

    public void CloseGridButton()
    {
        int boardNumber = mainController.GridFocused-1;

        GameGrids[boardNumber].GetComponent<Manager>().UnselectGrid();

        //switch (boardNumber)
        //{
        //    case 1: GameGrid1.UnselectGrid(); break;

        //}
    }

    public void ResetGridButton()
    {
        int boardNumber = mainController.GridFocused-1;
        GameGrids[boardNumber].GetComponent<Manager>().ResetGrid();
        //switch (boardNumber)
        //{
        //    case 1: GameGrid1.ResetGrid(); break;

        //}
    }

    public void ExportGridButton()
    {
        int boardNumber = mainController.GridFocused-1;

        if (gridExportsReady)
        {
            ActiveCountdownTimer();
            GameGrids[boardNumber].GetComponent<Manager>().ExportHighestNumberFromGrid();

        }
       // GameGrids[boardNumber].GetComponent<Manager>().ExportHighestNumberFromGrid();
        //switch (boardNumber)
        //{
        //    case 1: GameGrid1.ExportHighestNumberFromGrid(); break;

        //}
    }

    private void ActiveCountdownTimer()
    {
        //called to acitvate ienum timer, forces set amount of time wait between inputs to prevent spamming 
        gridExportsReady = false;
        // Debug.Log("GMR: " + gridMovementReady);
        StartCoroutine("MovementCountdownTimer", TimeBetweenExports);


    }
    private IEnumerator MovementCountdownTimer(int time)
    {
      //  float timer = time;
        while (TimeBetweenExportsTimer > 0)
        {
            yield return new WaitForSeconds(1);
            TimeBetweenExportsTimer--;
        }
        gridExportsReady = true;
        TimeBetweenExportsTimer = TimeBetweenExports;
        // Debug.Log("GMR: " + gridMovementReady);
    }
}