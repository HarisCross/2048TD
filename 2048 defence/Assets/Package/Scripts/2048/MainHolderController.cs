using UnityEngine;

public class MainHolderController : MonoBehaviour
{
    //public GameObject Grid1;
    // public GameObject Grid2;
    // public GameObject Grid3;
    // public GameObject Grid4;

    public GameObject CentrePos;
    public GameObject interactButtonsHolder;

    public int GridFocused = 0;//0 is none, 1-4 are different grids
    public bool GridSelected = false;
    public bool AnimationOccuring = false;//set to true whenever ani is happeneing and stop inputs when true to avoid issue with conflicting inputs

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (GridFocused == 0)
        {
            interactButtonsHolder.SetActive(false);
        }
        else
        {
            interactButtonsHolder.SetActive(true);
        }
    }
    public void SetCurrentGrid(int newGrid)
    {
        if(GridFocused == 0)
        {
            //no grid active - open a grid
            GridFocused = newGrid;
        }
        else
        {
            //grid active - close grids
            GridFocused = 0;
        }
      //  GridFocused = newGrid;

    }
}