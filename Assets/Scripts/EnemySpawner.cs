using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab;

    [Header("Behaviours")]
    [SerializeField] SpawningBehaviour spawningBehaviour;

    [Header("Spawn parameters")]
    [SerializeField] Vector3 targetPosition;

    [Header("Events")]
    public static System.Action<GameObject> onEnemySpawned;

    private void Awake()
    {
        spawningBehaviour = GetComponent<SpawningBehaviour>();
    }

    void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("EnemyGoal").transform.position;
    }

    private void OnEnable()
    {
        spawningBehaviour.onSpawn += SpawnEnemy; 
    }

    public void StartSpawning()
    {
        spawningBehaviour.StartSpawning();
    }

    private void OnDisable()
    {
        spawningBehaviour.onSpawn -= SpawnEnemy;
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyController>().SetTargetPosition(targetPosition);
        EnemyController.AddEnemy(newEnemy);
        //onEnemySpawned?.Invoke(newEnemy);
    }
}
