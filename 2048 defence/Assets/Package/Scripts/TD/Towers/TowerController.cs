using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject targetCrosshairs;

    private float rotateSpeed = 10f;
    private bool audioRotating = false;

    public bool currentlyShooting = false;
    private Vector3 targetVector;
    private float angleToRotate;
    private Quaternion amountToRotate;

    [Header("AudioClips/Sources")]
    public List<AudioClip> audioClipsShots = new List<AudioClip>();
    public AudioSource audioSourceFiring;
    public AudioSource audioSourceTurning;

    [Header("Shot/costs")]
    public int AMOUNTTOSTARTWITH = 0;
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
        //ModifyFirerateAndShotpower();
        AddResources(AMOUNTTOSTARTWITH);
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
            audioRotating = false;
           UpdateTarget();
            //CancelInvoke();
            //currentlyShooting = false;
        }
    }
    void UpdateCrosshairsTarget(GameObject newTarget)
    {
        //sends the towers target to the crosshairs

        if(newTarget != targetCrosshairs.GetComponent<CrosshairController>().target)
        {
            targetCrosshairs.GetComponent<CrosshairController>().AddTarget(newTarget);
        }


    }
    public void UpdateFirerateAndShotpower()
    {
        CancelInvoke("ShootAtTarget");
        InvokeRepeating("ShootAtTarget", turretShootingStartDelay, turretShootingDelay);

    }
    public void AddResources(int amountToAdd)
    {

        //adds values to storage, changes power and firerate then restarts repeating fire func to change fire rate
        shotResources += amountToAdd;
        ModifyFirerateAndShotpower(amountToAdd);
        UpdateFirerateAndShotpower();

    }
    private void ModifyFirerateAndShotpower(int amountAdded)
    {
        //call after every shot to change firerate and power of next shot
        

        //change turretshootingdelay ( 1f) and costpershot ( 2f)
        switch (amountAdded)
        {
            case 1:
                costPerShot = 0f;
                turretShootingDelay = 0f;
                break;
            case 2:
                costPerShot = 1f;
                turretShootingDelay = 0.5f;
                break;
            case 4:
                costPerShot = 1f;
                turretShootingDelay = 0.5f;
                break;
            case 8:
                costPerShot = 2f;
                turretShootingDelay = 0.75f;
                break;
            case 16:
                costPerShot = 2f;
                turretShootingDelay = 0.75f;
                break;
            case 32:
                costPerShot = 4f;
                turretShootingDelay = 1f;
                break;
            case 64:
                costPerShot = 8f;
                turretShootingDelay = 1.25f;
                break;
            case 128:
                costPerShot = 16f;
                turretShootingDelay = 1.5f;
                break;
            case 256:
                costPerShot = 32f;
                turretShootingDelay = 1.75f;
                break;
            case 512:
                costPerShot = 64f;
                turretShootingDelay = 2f;
                break;
            case 1024:
                costPerShot = 128f;
                turretShootingDelay = 3f;
                break;
            case 2048:
                costPerShot = 256f;
                turretShootingDelay = 4f;
                break;


        }


    }
    private void RotateTowardEnemy()
    {
        audioRotating = true;

        targetVector = CurrentTarget.transform.position - transform.position;
        angleToRotate = (Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg) - 90;

        amountToRotate = Quaternion.AngleAxis(angleToRotate, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, amountToRotate, Time.deltaTime * rotateSpeed);

    }

    private void UpdateTarget()
    {
        audioPlayRotating();
        //assign a current target from the list of targets
        //  print("assgined target");
        if (listOfTargets.Count > 0)
        {
            CurrentTarget = listOfTargets[0];

        }
        else
        {
            CurrentTarget = null;

        }
        UpdateCrosshairsTarget(CurrentTarget);
    }

    private void ShootAtTarget()
    {

        // currentlyShooting = true;

        float shotValue = 0f;

        if(shotResources >= costPerShot && CurrentTarget != null)
        {
            print("firing");
            shotValue = costPerShot;
            shotResources -= costPerShot;

            
                GameObject tempBullet = Instantiate(Resources.Load<GameObject>("Shot"), transform.position, transform.rotation);
                tempBullet.transform.parent = bulletHolder.transform;

                tempBullet.gameObject.GetComponent<BulletController>().AddValues(CurrentTarget, shotValue);

          //  ModifyFirerateAndShotpower();
            audioPlayShotFired();

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

    void audioPlayRotating()
    {
        //check if already playing and if turret is rotating
        //if rotating and not playing then start
        //if not rotating and playing then stop

        bool playing = audioSourceTurning.isPlaying;
        if (listOfTargets.Count > 0)
        {

            //if (audioRotating == true)
            //{
            //    //rotating

            //    if (playing == false)
            //    {
                    //not already playing
                    audioSourceTurning.Play();

                //}


            }
            else
            {
            //    //not rotating

            //    if (playing == true)
            //    {
                    //not already playing
                    audioSourceTurning.Stop();

            //    }

            //}
        }
    }
    void audioPlayShotFired()
    {
        //print("playing firing clip");
        audioSourceFiring.PlayOneShot(audioClipsShots[Random.Range(0, audioClipsShots.Count)]);

    }
}