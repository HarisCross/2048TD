using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNumberDisplay : MonoBehaviour {

    BulletController bulletController;


    // Use this for initialization
    void Start () {
        bulletController = transform.parent.gameObject.GetComponent<BulletController>();
	}
	
	// Update is called once per frame
	void Update () {
		



	}
}
