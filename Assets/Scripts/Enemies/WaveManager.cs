using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance { get; private set; }

    [Header("Wave parameters")]
    [SerializeField] float waveInterval = 5f;
    public int numberOfWaves = 3;

    [SerializeField] List<EnemySpawner> spawners;

    public int currentWave = 0;
    WaitForSeconds waveIntervalWait;

    void Awake()
    {
        //Ensure singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("EnemySpawner");
        for (int i = 0; i < spawnerObjects.Length; i++)
        {
            spawners.Add(spawnerObjects[i].GetComponent<EnemySpawner>());
        }

        waveIntervalWait = new WaitForSeconds(waveInterval);
        StartCoroutine(SpawnWavesCoroutine());
    }

    IEnumerator SpawnWavesCoroutine()
    {
        while (true)
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.StartSpawning(currentWave);
            }
            UpdateWaveCount();

            yield return waveIntervalWait;
        }
    }

    void UpdateWaveCount()
    {
        currentWave++;
        if (currentWave >= numberOfWaves)
        {
            StopSpawning();
        }
    }

    void StopSpawning()
    {
        StopAllCoroutines();
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }
}
