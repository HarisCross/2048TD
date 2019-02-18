using UnityEngine;

public class GridButton : MonoBehaviour
{
    public MainHolderController mainController;
    public int thisGrid;

    public void ActivateGridbuttons()
    {
    //    mainController.GridFocused = thisGrid;
        mainController.SetCurrentGrid(thisGrid);
    }
    private void Update()
    {
      //  print("button at: " + this.transform.position);
    }
}