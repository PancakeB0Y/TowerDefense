using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawningBehaviour : MonoBehaviour
{
    [Header("Spawn parameters")]
    [SerializeField] protected float spawnTimer = 1f;

    public System.Action onSpawn;

    public abstract void StartSpawning();
}
