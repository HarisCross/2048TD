using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinionDetails : MonoBehaviour {


    public float defaultScaleSizeCurrent = 5f;
    public float defaultScaleSizeMin = 0.1f;

    private bool scaleForHealth = false;

    public float healthMax = 0;
    public float healthCurrent = 0;

    float percen;
    float scaleToAimFor;
    float amount;

    private GameObject healthChild;

    [SerializeField]
    private bool movingMinion = false;

    public MinionController parent;
    public SpawnerController spawner;

    private GameObject thisTextUIDisplay;

    public int CurrentNode = -1;
    public Vector2 NextPosition,StartPosition;
    public float timer = 0f, moveSpeed = 1f;


    void Start()
    {
        StartPosition = transform.position;
        healthMax = healthCurrent;
        healthChild = transform.GetChild(0).transform.gameObject;
        //SpawnUIDisplay();
    } 
	
	// Update is called once per frame
	void Update () {

        defaultScaleSizeCurrent = healthChild.transform.localScale.x;

        if(NextPosition != null)
        {
            MoveMinion();
          
        }

        if(healthCurrent < 1) {
          //  print("minion killed : " + healthCurrent);
            Destroy(this.transform.gameObject);
        }

        if (scaleForHealth == true) ScaleHealth();

	}

    public void DoDamage(float damage)
    {
        //use to deal damage to this go
        healthCurrent -= damage;

        percen = healthCurrent / healthMax;
        scaleToAimFor = percen * defaultScaleSizeCurrent;
        amount = 3f;

        scaleForHealth = true;

    }
    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.currWaveDetails.CurrentMinionsInPlay.Remove(transform.gameObject);
            spawner.currWaveDetails.CheckMinionsRemainingInWave();
        }
        Destroy(thisTextUIDisplay);
    }

    public void MoveMinion()
    {
        timer += Time.deltaTime * moveSpeed;

       transform.position =  Vector2.Lerp(StartPosition, NextPosition, timer);

        if(Vector2.Distance(transform.position,NextPosition) < 0.1f)
        {
            spawner.UpdateMinionTarget(transform.gameObject);
        }


    }
   
    private void SpawnUIDisplay()
    {
        //spawn ui text over this minion, pass it this gameobject to follow

        thisTextUIDisplay = Instantiate(Resources.Load<GameObject>("CountUIDisplay"), transform.position, transform.rotation);
        thisTextUIDisplay.GetComponent<CountUIDisp>().parent = transform.gameObject;
        thisTextUIDisplay.transform.parent = GameObject.Find("MinionValueUI").transform;

        Vector3 newScale = new Vector3(1, 1, 1);

        thisTextUIDisplay.transform.localScale = newScale;
    }

   private void ScaleHealth()
    {
        

       // print("percen: " + percen + ". scale: " + scaleToAimFor);
      //  do
     //   {
            //Vector3 newScale = new Vector3((defaultScaleSizeCurrent - Time.deltaTime), (defaultScaleSizeCurrent - Time.deltaTime), 1);
            //healthChild.transform.localScale = newScale;

            healthChild.transform.localScale -= new Vector3((amount * Time.deltaTime), (amount * Time.deltaTime), 0);
            defaultScaleSizeCurrent = healthChild.transform.localScale.x;



        //  } while (defaultScaleSizeCurrent > scaleToAimFor);

        if (defaultScaleSizeCurrent < scaleToAimFor) scaleForHealth = false;

        //get percentage of current health and scale white accordingly
        

    }


}
