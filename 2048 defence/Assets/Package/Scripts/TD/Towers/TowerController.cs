using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
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
        ModifyFirerateAndShotpower();
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
            audioRotating = false;
           UpdateTarget();
            //CancelInvoke();
            //currentlyShooting = false;
        }
    }
    private void AssignAudioSources()
    {



    }
    private void ModifyFirerateAndShotpower()
    {
        //call after every shot to change firerate and power of next shot

        //change turretshootingdelay ( 1f) and costpershot ( 2f)
        if (shotResources < 10f)
        {
            


        }
        else
        {
            if (shotResources < 25f)
            {
                
            }
            else
            {
                if (shotResources < 50f)
                {
                    
                }
                else
                {
                    if (shotResources < 100f)
                    {
                        
                    }
                    else
                    {
                        
                    }
                }
            }
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
    void ChangeFireRate(int newFirerate)
    {
        CancelInvoke("ShootAtTarget");

        turretShootingDelay = newFirerate;

        InvokeRepeating("ShootAtTarget", turretShootingStartDelay, turretShootingDelay);
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

            ModifyFirerateAndShotpower();
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