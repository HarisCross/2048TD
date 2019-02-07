﻿using System.Collections.Generic;
using UnityEngine;

public class WaveDetails : MonoBehaviour
{
    public List<GameObject> Minions = new List<GameObject>();
    public List<GameObject> CurrentMinionsInPlay = new List<GameObject>();

    public SpawnerController spawnContrller;

    public int WaveNumber = 1;
    //private int minionSpawnMax = 5;

    public float waveStartDelay = 0f;
    public float spawnMinionDelay = 0.5f;
    public float waveMinionMax = 5f;
    public float waveMinionSpeed = 1f;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void CheckMinionsRemainingInWave()
    {
        //called on minion death to check count of currMins, if 0 then they all must be dead or my code is worse than i thought

        if (CurrentMinionsInPlay.Count == 0)
        {
            spawnContrller.levelManager.WaveEnded(spawnContrller, spawnContrller.currentWave);
        }
    }
}