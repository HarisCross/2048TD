using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    private GameObject DDOLGO;//dont destory on load gameobject which holds its scripts
    private LevelManager levManager;

    private GameObject tutEndButton;

	// Use this for initialization
	void Start () {

        DDOLGO = GameObject.Find("DDOLScripts");
        levManager = DDOLGO.GetComponent<LevelManager>();


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
    