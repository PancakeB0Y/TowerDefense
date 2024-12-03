using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance { get; private set; }

    [Header("Wave parameters")]
    [SerializeField] float waveInterval = 5f;
    [SerializeField] int numberOfWaves = 3;

    [SerializeField] List<EnemySpawner> spawners;

    int currentWave = 0;

    //Ensure singleton
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("EnemySpawner");
        for (int i = 0; i < spawnerObjects.Length; i++)
        {
            spawners.Add(spawnerObjects[i].GetComponent<EnemySpawner>());
        }

        InvokeRepeating(nameof(StartWave), 0, waveInterval);
    }

    void StartWave()
    {
        foreach(EnemySpawner spawner in spawners)
        {
            spawner.StartSpawning();
        }
        UpdateWaveCount();
    }

    void UpdateWaveCount()
    {
        currentWave++;
        if (currentWave >= numberOfWaves)
        {
            CancelInvoke(nameof(StartWave));
        }
    }
}
