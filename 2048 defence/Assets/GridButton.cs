using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButton : MonoBehaviour
{

    public MainHolderController mainController;
    public int thisGrid;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateGridbuttons()
    {
        mainController.GridFocused = thisGrid;

    }
}
