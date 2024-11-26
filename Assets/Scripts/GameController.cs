using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<GameObject> enemies;

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
        }
    }

    void Start()
    {
        enemies = new List<GameObject>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        
    }

    public void OnEnemySpawned(GameObject enemy, Vector3 targetPosition)
    {
        enemy.GetComponent<EnemyController>().SetTargetPosition(targetPosition);
        enemies.Add(enemy);
    }

    public void OnEnemyTargetReached(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }
}
