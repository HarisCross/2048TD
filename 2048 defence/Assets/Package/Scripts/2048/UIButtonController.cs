using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonController : MonoBehaviour {

    public MainHolderController mainController;

    public Manager GameGrid1;
	// Use this for initialization
	void Start () {
        mainController = transform.GetComponent<MainHolderController>();
        GameGrid1.UnselectGrid();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseGridButton()
    {
        int boardNumber = mainController.GridFocused;

        switch (boardNumber)
        {
            case 1: GameGrid1.UnselectGrid(); break;



        }

    }
    public void ResetGridButton()
    {
        int boardNumber = mainController.GridFocused;

        switch (boardNumber)
        {
            case 1: GameGrid1.ResetGrid(); break;



        }

    }
    public void ExportGridButton()
    {
        int boardNumber = mainController.GridFocused;

        switch (boardNumber)
        {
            case 1: GameGrid1.ExportHighestNumberFromGrid(); break;



        }

    }
}
