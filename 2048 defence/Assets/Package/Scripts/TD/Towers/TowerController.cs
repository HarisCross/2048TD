using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private float rotateSpeed = 10f;

    public bool currentlyShooting = false;
    private Vector3 targetVector;
    private float angleToRotate;
    private Quaternion amountToRotate;

    [Header("Shot/costs")]
    public float shotResources;

    public float costPerShot = 2f;

    public float turretShootingDelay = 1f;
    public float turretShootingStartDelay = 1f;

    [Header("Targeting")]
    public GameObject CurrentTarget;

    public List<GameObject> listOfTargets = new List<GameObject>();

    [Header("Gameobjects")]
    public GameObject Bullet;

    public GameObject bulletHolder;

    // Use this for initialization
    private void Start()
    {
        InvokeRepeating("ShootAtTarget", turretShootingStartDelay, turretShootingDelay);
    }

    // Update is called once per frame
    private void Update()
    {
        if (CurrentTarget != null)
        {
            RotateTowardEnemy();
        }
        else
        {
           UpdateTarget();
            //CancelInvoke();
            //currentlyShooting = false;
        }
    }

    private void RotateTowardEnemy()
    {
        targetVector = CurrentTarget.transform.position - transform.position;
        angleToRotate = (Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg) - 90;

        amountToRotate = Quaternion.AngleAxis(angleToRotate, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, amountToRotate, Time.deltaTime * rotateSpeed);
    }

    private void UpdateTarget()
    {
        //assign a current target from the list of targets
      //  print("assgined target");
      if(listOfTargets.Count > 0)
        {
            CurrentTarget = listOfTargets[0];

        }
        else
        {
            CurrentTarget = null;

        }
       
    }

    private void ShootAtTarget()
    {
      //  print("Shooting");
        // currentlyShooting = true;

        float shotValue = 0f;

        if(shotResources >= costPerShot && CurrentTarget != null)
        {
            shotValue = costPerShot;
            shotResources -= costPerShot;

            
                GameObject tempBullet = Instantiate(Resources.Load<GameObject>("Shot"), transform.position, transform.rotation);
                tempBullet.transform.parent = bulletHolder.transform;

                tempBullet.gameObject.GetComponent<BulletController>().AddValues(CurrentTarget, shotValue);

            


        }


        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        listOfTargets.Add(collision.transform.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        listOfTargets.Remove(collision.transform.gameObject);
       UpdateTarget();
    }
}