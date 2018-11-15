using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButton : MonoBehaviour
{

    public MainHolderController mainController;
    public int thisGrid;

    public void ActivateGridbuttons()
    {
        mainController.GridFocused = thisGrid;

    }
}
