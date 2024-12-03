using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalSpawningBehaviour : SpawningBehaviour
{
    [SerializeField] int spawnCount = 3;
    int spawnedEnemiesCount = 0;

    public override void StartSpawning()
    {
        spawnedEnemiesCount = 0;
        InvokeRepeating(nameof(Spawn), 0, spawnTimer);
    }

    void Spawn()
    {
        onSpawn.Invoke();
        UpdateSpawnCount();
    }

    void UpdateSpawnCount()
    {
        spawnedEnemiesCount++;
        if (spawnedEnemiesCount >= spawnCount)
        {
            CancelInvoke(nameof(Spawn));
        }
    }
}
