using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefab")]
    [SerializeField] GameObject enemyPrefab;

    [Header("Spawn parameters")]
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] int spawnCount = 3;
    [SerializeField] Vector3 targetPosition;

    [Header("Events")]
    public UnityEvent<GameObject, Vector3> onEnemySpawned;

    int spawnedEnemiesCount = 0;
    
    void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("EnemyTarget").transform.position;
        onEnemySpawned.AddListener(GameController.instance.OnEnemySpawned);

        InvokeRepeating("SpawnEnemy", 0, spawnTimer);
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        onEnemySpawned?.Invoke(newEnemy, targetPosition);

        UpdateSpawnCount();
    }

    void UpdateSpawnCount()
    {
        spawnedEnemiesCount++;
        if (spawnedEnemiesCount >= spawnCount)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
}
