using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] GameObject enemyPrefab2;

    [Header("Behaviours")]
    [SerializeField] SpawningBehaviour spawningBehaviour;

    [Header("Spawn parameters")]
    [SerializeField] Vector3 targetPosition;
    [SerializeField] List<WaveData> waves;

    public static System.Action<GameObject> onEnemySpawned;

    List<GameObject> enemiesToSpawn;
    System.Random r = new System.Random();

    private void Awake()
    {
        spawningBehaviour = GetComponent<SpawningBehaviour>();
    }

    void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("EnemyGoal").transform.position;

        waves = GetComponents<WaveData>().ToList();

        waves = waves.OrderBy(x=>x.index).ToList();

        enemiesToSpawn = new List<GameObject>();
    }

    private void OnEnable()
    {
        spawningBehaviour.onSpawn += SpawnEnemy; 
    }

    public void StartSpawning(int waveIndex)
    {
        //Set data for the current wave
        spawningBehaviour.SetWaveData(waves[waveIndex]);
        UpdateEnemiesToSpawn(waveIndex);

        spawningBehaviour.StartSpawning();
    }

    public void StopSpawning()
    {
        spawningBehaviour.StopSpawning();
    }

    private void OnDisable()
    {
        spawningBehaviour.onSpawn -= SpawnEnemy;
    }

    void UpdateEnemiesToSpawn(int waveIndex)
    {
        enemiesToSpawn.Clear();

        for(int i = 0; i < waves[waveIndex].enemy1Amount; i++)
        {
            enemiesToSpawn.Add(enemyPrefab1);
        }

        for (int i = 0; i < waves[waveIndex].enemy2Amount; i++)
        {
            enemiesToSpawn.Add(enemyPrefab2);
        }

        //Order the list randomly
        enemiesToSpawn = enemiesToSpawn.OrderBy(x => r.Next()).ToList();
    }

    void SpawnEnemy()
    {
        GameObject enemyPrefab = enemiesToSpawn[enemiesToSpawn.Count - 1];
        enemiesToSpawn.RemoveAt(enemiesToSpawn.Count - 1);

        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyController>().SetTargetPosition(targetPosition);
        EnemyController.AddEnemy(newEnemy);
        //onEnemySpawned?.Invoke(newEnemy);
    }
}
