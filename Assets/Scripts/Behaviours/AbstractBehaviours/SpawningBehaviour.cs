using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawningBehaviour : MonoBehaviour
{
    [Header("Spawn parameters")]
    protected float spawnInterval = 1f;

    public System.Action onSpawn;
    public System.Action onWaveFinished;

    public abstract void StartSpawning();
    public abstract void StopSpawning();

    public abstract void SetWaveData(WaveData waveData);
}
