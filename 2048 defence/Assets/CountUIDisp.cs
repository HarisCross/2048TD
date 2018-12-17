using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountUIDisp : MonoBehaviour {

     public GameObject parent;

    private Vector2 screenPos;
   // [SerializeField]
   // private Camera cam;
    private MinionDetails minCont;
    private BulletController bullCont;
    private TextMeshProUGUI textDisplayed;

	// Use this for initialization
	void Start () {

        if (parent.GetComponent<MinionDetails>()) minCont = parent.GetComponent<MinionDetails>();
        if (parent.GetComponent<BulletController>()) bullCont = parent.GetComponent<BulletController>();
        textDisplayed = transform.GetComponent<TextMeshProUGUI>();

        //cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        //print(transform.position.z);
        UpdateValue();
        MoveWithParent();
      //  print(transform.position.z);
    }

    private void MoveWithParent()
    {
        //moves the ui over the parent during duration
        //screenPos = cam.WorldToScreenPoint(parent.transform.position);
        //print(screenPos.x + " : " + screenPos.y + " : ");
        //transform.position = screenPos;

        //transform.position = Camera.main.WorldToScreenPoint(parent.transform.position);


        //transform.position = parent.transform.position;

        Vector3 zOffset = new Vector3(parent.transform.position.x, parent.transform.position.y,0);

       // print(zOffset.z);
        transform.position = zOffset;
    }
    private void UpdateValue()
    {
        //changes number displayed using parent

        if(minCont != null)
        {
            textDisplayed.text = minCont.healthCurrent.ToString();

        }

        if (bullCont != null)
        {
            textDisplayed.text = bullCont.bulletValue.ToString();

        }
    }

}
