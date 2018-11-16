using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinionDetails : MonoBehaviour {



    public int health = 0;
    [SerializeField]
    private bool movingMinion = false;

    public MinionController parent;
    public SpawnerController spawner;


    public int CurrentNode = -1;
    public Vector2 NextPosition,StartPosition;
    public float timer = 0f, moveSpeed = 1f;


    void Start()
    {
        StartPosition = transform.position;
    } 
	
	// Update is called once per frame
	void Update () {

        if(NextPosition != null)
        {
            MoveMinion();
           // print(NextPosition);
        }


	}


    private void OnDestroy()
    {
        spawner.Minions.Remove(transform.gameObject);
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
   




}
