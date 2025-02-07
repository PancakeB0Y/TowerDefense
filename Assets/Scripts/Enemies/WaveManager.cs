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

    public System.Action onWaveFinished;
    public System.Action onLastWaveFinished;

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

        currentWave = 0;
        spawners = new List<EnemySpawner>();
    }

    private void Start()
    {
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("EnemySpawner");
        for (int i = 0; i < spawnerObjects.Length; i++)
        {
            EnemySpawner curEnemySpawner = spawnerObjects[i].GetComponent<EnemySpawner>();
            if (curEnemySpawner != null)
            {
                spawners.Add(curEnemySpawner);
            }
        }

        currentWave = 0;

        waveIntervalWait = new WaitForSeconds(waveInterval);
    }

    private void OnEnable()
    {
        EnemySpawner.onWaveFinished += FinishWave;
    }

    private void OnDisable()
    {
        EnemySpawner.onWaveFinished -= FinishWave;
    }

    public void StartNextWave()
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.StartSpawning(currentWave);
        }
        UpdateWaveCount();
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
            onLastWaveFinished?.Invoke();
        }
    }

    public bool isLastWaveSpawned()
    {
        return currentWave >= numberOfWaves;
    }

    void StopSpawning()
    {
        StopAllCoroutines();
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }

    void FinishWave()
    {
        onWaveFinished?.Invoke();
    }
}
