using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalSpawningBehaviour : SpawningBehaviour
{
    int spawnCount;
    int spawnedEnemiesCount;

    WaitForSeconds spawnIntervalWait;

    private void Start()
    {
        spawnIntervalWait = new WaitForSeconds(spawnInterval);
    }

    public override void StartSpawning()
    {
        spawnedEnemiesCount = 0;
        StartCoroutine(SpawnCoroutine());
    }

    public override void StopSpawning()
    {
        StopAllCoroutines();
    }

    public override void SetWaveData(WaveData waveData)
    {
        spawnInterval = waveData.spawnInterval;

        spawnCount = waveData.enemy1Amount + waveData.enemy2Amount;
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    protected override void Spawn()
    {
        if (spawnedEnemiesCount >= spawnCount)
        {
            onWaveFinished?.Invoke();
            StopAllCoroutines();
            return;
        }

        onSpawn?.Invoke();
        UpdateSpawnCount();
    }

    void UpdateSpawnCount()
    {
        spawnedEnemiesCount++;
    }
}
