using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TUIController : MonoBehaviour {

    public GameObject TextGO;
    private TextMeshProUGUI textComp;
    private TowerController towCont;

	// Use this for initialization
	void Start () {

        textComp = TextGO.GetComponent<TextMeshProUGUI>();
        towCont = transform.GetComponent<TowerController>();
	}
	
	// Update is called once per frame
	void Update () {

        CountCheck();


    }
    private void CountCheck()
    {
        //if there is no ammo display that

        if(towCont.shotResources == 0)
        {

            textComp.text = "NO AMMO";

        }
        else
        {
            textComp.text = towCont.shotResources.ToString();

        }

    }
}
