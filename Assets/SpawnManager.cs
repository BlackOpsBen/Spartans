using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<Spawner> spawners = new List<Spawner>();

    [SerializeField] private Transform enemiesParent;

    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();

    [SerializeField] private float waveInterval = 10.0f;
    [SerializeField] private int startingWaveSize = 5;
    private int currentWaveSize = 0;
    [SerializeField] private int increaseWaveSizeEveryNWaves = 1;
    private int nWaveCounter = 0;
    [SerializeField] private int waveSizeIncrement = 1;
    private float waveTimer = float.MaxValue;
    [SerializeField] private float minWaveDelay = 3.0f;
    private bool minDelayTriggered = false;

    private int currentWave = 0;

    [SerializeField] float spawnOffset = 1.0f;

    private void Start()
    {
        waveTimer = waveInterval - minWaveDelay;
        currentWaveSize = startingWaveSize;
        spawners.AddRange(FindObjectsOfType<Spawner>());
    }

    private void Update()
    {
        waveTimer += Time.deltaTime;

        if (waveTimer > waveInterval)
        {
            StartNextWave();
        }
        else if (!minDelayTriggered && enemiesParent.childCount == 0 && (waveInterval - waveTimer) > minWaveDelay)
        {
            waveTimer = waveInterval - minWaveDelay;
            minDelayTriggered = true;
        }
    }

    private void StartNextWave()
    {
        waveTimer = 0.0f;
        SpawnWave();
        currentWave++;
        IncreaseWaveDifficulty();
        minDelayTriggered = false;
    }

    private void SpawnWave()
    {
        int randSpawnMode = UnityEngine.Random.Range(0, spawners.Count + 1);

        // Spawn randomly
        if (randSpawnMode >= spawners.Count)
        {
            for (int i = 0; i < currentWaveSize; i++)
            {
                int randomSpawner = UnityEngine.Random.Range(0, spawners.Count);
                spawners[randomSpawner].Spawn(enemyPrefabs[0], spawnOffset * i, enemiesParent);
            }
        }
        // Spawn together
        else
        {
            int randomSpawner = UnityEngine.Random.Range(0, spawners.Count);
            for (int i = 0; i < currentWaveSize; i++)
            {
                spawners[randomSpawner].Spawn(enemyPrefabs[0], spawnOffset * i, enemiesParent);
            }
        }
    }

    private void IncreaseWaveDifficulty()
    {
        nWaveCounter++;
        if (nWaveCounter == increaseWaveSizeEveryNWaves)
        {
            nWaveCounter = 0;
            currentWaveSize += waveSizeIncrement;
        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
