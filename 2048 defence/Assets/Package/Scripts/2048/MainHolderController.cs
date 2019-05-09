using UnityEngine;
using System.Collections.Generic;

public class MainHolderController : MonoBehaviour
{
    //public GameObject Grid1;
    // public GameObject Grid2;
    // public GameObject Grid3;
    // public GameObject Grid4;

    public GameObject CentrePos;
    public GameObject interactButtonsHolder;
    private bool interactButtonsOpen = false;

    public int GridFocused = 0;//0 is none, 1-4 are different grids
    public bool GridSelected = false;
    public bool AnimationOccuring = false;//set to true whenever ani is happeneing and stop inputs when true to avoid issue with conflicting inputs

    public List<GameObject> GridsList = new List<GameObject>();

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //if (GridFocused == 0)
        //{
        //    interactButtonsHolder.SetActive(false);
        //}
        //else
        //{
        //    interactButtonsHolder.SetActive(true);
        //}
    }
    public void SetCurrentGrid(int newGrid)
    {
        //if(GridFocused == 0)
        //{
        //    //no grid active - open a grid
        //    GridFocused = newGrid;
        //}
        //else
        //{
        //    //grid active - close grids
        //    GridFocused = 0;
        //}
        //  GridFocused = newGrid;

       // print("actived SetCurrentGrid");

        if (AnimationOccuring) return;
        if (GridsList.Count == 0) return;

        if (interactButtonsOpen)
        {
          //  print("should close menu");

            ///interactButtonsHolder.GetComponent<Animator>().Play("CloseGridButton");
            //interactButtonsHolder.GetComponent<Animator>().SetBool("MenuActiveState", false);
            //interactButtonsHolder.GetComponent<Animator>().SetTrigger("MenuActiveStateTrigger");
            interactButtonsOpen = false;
        }
        else
        {
            // interactButtonsHolder.GetComponent<Animator>().Play("OpenGridButton");

          //  print("Should open menu");

            //interactButtonsHolder.GetComponent<Animator>().SetBool("MenuActiveState", true);
            //interactButtonsHolder.GetComponent<Animator>().SetTrigger("MenuActiveStateTrigger");
            interactButtonsOpen = true;

        }


        if (GridFocused == 0)// if no grid open then open using param else close open grid
        {
            //print("open grid");
            GridsList[newGrid-1].GetComponent<Manager>().SelectGrid();
            GridFocused = newGrid;



        }
        else
        {

            GridsList[GridFocused-1].GetComponent<Manager>().UnselectGrid();
            GridFocused = 0;

        }

        //if (GridFocused == newGrid)
        //{
        //    //if grid button clicked is for the current active grid - should close grid

        //   // print("close grid");

        //}

    }
}