using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<Spawner> spawners = new List<Spawner>();

    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();

    [SerializeField] private float waveInterval = 5.0f;
    [SerializeField] private int startingWaveSize = 5;
    private int currentWaveSize = 0;
    [SerializeField] private int increaseWaveSizeEveryNWaves = 1;
    private int nWaveCounter = 0;
    [SerializeField] private int waveSizeIncrement = 1;
    private float waveTimer = 0.0f;

    private int currentWave = 0;

    [SerializeField] float spawnOffset = 1.0f;

    private void Start()
    {
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
    }

    private void StartNextWave()
    {
        waveTimer = 0.0f;
        SpawnWave();
        currentWave++;
        IncreaseWaveDifficulty();
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
                spawners[randomSpawner].Spawn(enemyPrefabs[0], spawnOffset * i);
            }
        }
        // Spawn together
        else
        {
            int randomSpawner = UnityEngine.Random.Range(0, spawners.Count);
            for (int i = 0; i < currentWaveSize; i++)
            {
                spawners[randomSpawner].Spawn(enemyPrefabs[0], spawnOffset * i);
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
