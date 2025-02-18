using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance { get; private set; }

    [Header("Wave parameters")]
    public int numberOfWaves = 3;

    [Header("Debug field")]
    [SerializeField] List<EnemySpawner> spawners;

    [HideInInspector] public int currentWave { get; private set; } = 0;

    [SerializeField] bool spawnOnStart = false;

    public System.Action onWaveFinished;
    public System.Action onLastWaveFinished;

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

        if (spawnOnStart)
        {
            StartNextWave();
        }
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
        if (isLastWaveSpawned()) {
            StopSpawning();
            return;
        }

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.StartSpawning(currentWave);
        }
        UpdateWaveCount();
    }

    void UpdateWaveCount()
    {
        currentWave++;
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
        if (isLastWaveSpawned())
        {
            StopSpawning();
            onLastWaveFinished?.Invoke();
            return;
        }

        onWaveFinished?.Invoke();
    }
}
