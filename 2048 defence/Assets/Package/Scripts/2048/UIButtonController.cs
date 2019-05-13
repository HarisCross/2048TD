using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    public MainHolderController mainController;

    public List<GameObject> GameGrids = new List<GameObject>();

    [SerializeField]
    private float TimeBetweenExports = 5f;
    //[SerializeField]
    //private float TimeBetweenExportsTimer = 5f;
    [SerializeField]
    private bool gridExportsReady = true;
    private bool exportButtonBeingFilled = true;// if false then is empty, if true then full 

    public GameObject exportButton;
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

        if (gridExportsReady && GameGrids[boardNumber].GetComponent<Manager>().exportedThisMovement == false)//needs to check if tis possible to export from manager currently in use
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

        //if (exportButtonBeingFilled)
        //{
        //    ExportButtonEmptyFillAmount();
        //}
        //else
        //{
        //    ExportButtonFillFillAmount();
        //}

    }
    private IEnumerator MovementCountdownTimer(int time)
    {
        //should empty the fill amount in 1 second then refill it over the next 4 seconds
        float timer = time;
       // print("timer is " + time);
        while (timer > 0)
        {
            yield return new WaitForSeconds(0.01f);

            timer -=0.01f;

            if(timer >= 4f)
            {
                       exportButton.GetComponent<Image>().fillAmount -= 0.01f;
            }
            else
            {
                        exportButton.GetComponent<Image>().fillAmount += 0.0025f;
            }

        }
        gridExportsReady = true;
      //  print("read to export again");
        //TimeBetweenExportsTimer = TimeBetweenExports;
    }

}