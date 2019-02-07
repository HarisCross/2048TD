using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour
{
    public MainHolderController mainController;

    public List<GameObject> GameGrids = new List<GameObject>();

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
        int boardNumber = mainController.GridFocused;

        GameGrids[boardNumber - 1].GetComponent<Manager>().UnselectGrid();

        //switch (boardNumber)
        //{
        //    case 1: GameGrid1.UnselectGrid(); break;

        //}
    }

    public void ResetGridButton()
    {
        int boardNumber = mainController.GridFocused;
        GameGrids[boardNumber - 1].GetComponent<Manager>().ResetGrid();
        //switch (boardNumber)
        //{
        //    case 1: GameGrid1.ResetGrid(); break;

        //}
    }

    public void ExportGridButton()
    {
        int boardNumber = mainController.GridFocused;
        GameGrids[boardNumber - 1].GetComponent<Manager>().ExportHighestNumberFromGrid();
        //switch (boardNumber)
        //{
        //    case 1: GameGrid1.ExportHighestNumberFromGrid(); break;

        //}
    }
}