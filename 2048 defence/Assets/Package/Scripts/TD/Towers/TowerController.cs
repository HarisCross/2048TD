using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    private float rotateSpeed = 10f;

    [Header("Shot/costs")]
    public int shotResources;
    public float costPerShot = 2f;


    [Header("Targeting")]
    public GameObject CurrentTarget;
    public List<GameObject> listOfTargets = new List<GameObject>();

    [Header("Gameobjects")]
    public GameObject Bullet;

	// Use this for initialization
	void Start () {

        InvokeRepeating("ShootAtTarget", 1f, 1f);


	}
	
	// Update is called once per frame
	void Update () {

        if(CurrentTarget != null) RotateTowardEnemy();



    }

    private void RotateTowardEnemy()
    {
        Vector3 targetVector;
        float angleToRotate;
        Quaternion amountToRotate;



         targetVector = CurrentTarget.transform.position - transform.position;
         angleToRotate = (Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg) - 90;

         amountToRotate = Quaternion.AngleAxis(angleToRotate, Vector3.forward);


        transform.rotation = Quaternion.Slerp(transform.rotation, amountToRotate, Time.deltaTime * rotateSpeed);



    }
    private void ShootAtTarget()
    {
        if(CurrentTarget != null)
        {
            //if you have target then fire








        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        listOfTargets.Add(collision.transform.gameObject);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        listOfTargets.Remove(collision.transform.gameObject);

    }
    private void UpdateCurrentTarget()
    {



    }
}
