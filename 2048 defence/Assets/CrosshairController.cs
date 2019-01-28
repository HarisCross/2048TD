using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {

    public GameObject target { get; private set; }
    private Vector3 newPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (target != null)
        {

            //this.transform.position = target.transform.position;
            newPos = target.transform.position;
            newPos.z = 0;
            this.transform.position = newPos;

            if (this.transform.GetComponent<SpriteRenderer>().enabled == false)
                this.transform.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            if (this.transform.GetComponent<SpriteRenderer>().enabled == true)
                this.transform.GetComponent<SpriteRenderer>().enabled = false;

        }



    }
    public void AddTarget(GameObject targ)
    {
        target = targ;//does what it says on the tin

    }
}
