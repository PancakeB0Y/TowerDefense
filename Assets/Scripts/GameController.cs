using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    public List<GameObject> enemies { get; private set; }

    Camera mainCamera;

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
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        enemies = new List<GameObject>();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        HPModel.onDeath += OnEnemyDeath;
        EnemyController.onTargetReached += OnEnemyTargetReached;
        EnemySpawner.onEnemySpawned += OnEnemySpawned;
    }

    private void OnDisable()
    {
        HPModel.onDeath -= OnEnemyDeath;
        EnemyController.onTargetReached -= OnEnemyTargetReached;
        EnemySpawner.onEnemySpawned -= OnEnemySpawned;
    }

    public void OnEnemySpawned(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void OnEnemyTargetReached(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
