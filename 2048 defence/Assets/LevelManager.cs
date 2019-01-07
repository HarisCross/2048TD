using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private List<GameObject> CurrentMinions = new List<GameObject>();

    public List<SpawnerController> spawners = new List<SpawnerController>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



		
	}

    public void WaveEnded(SpawnerController spawnController, int waveNumber)
    {
        //wave's minions have all died

        print(spawnController.transform.gameObject + "has ended wave by KO : " + waveNumber);

    }
    public void WaveComplete(SpawnerController spawnController, int waveNumber) 
    {
        //wave timer has triggered to end the wave and if poss start next one

        print(spawnController.transform.gameObject + " ended wave by time limit: " + waveNumber);

    }
}
