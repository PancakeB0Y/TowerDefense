using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab;

    [Header("Spawn parameters")]
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] int spawnCount = 3;
    [SerializeField] Vector3 targetPosition;

    [Header("Events")]
    public static System.Action<GameObject> onEnemySpawned;

    int spawnedEnemiesCount = 0;
    
    void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("EnemyTarget").transform.position;

        InvokeRepeating(nameof(SpawnEnemy), 0, spawnTimer);
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyController>().SetTargetPosition(targetPosition);
        onEnemySpawned?.Invoke(newEnemy);

        UpdateSpawnCount();
    }

    void UpdateSpawnCount()
    {
        spawnedEnemiesCount++;
        if (spawnedEnemiesCount >= spawnCount)
        {
            CancelInvoke(nameof(SpawnEnemy));
        }
    }
}
