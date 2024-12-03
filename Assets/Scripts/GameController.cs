using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    [SerializeField] HPModel hpModel;

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

        hpModel = GetComponent<HPModel>();
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        EnemyController.onTargetReached += OnEnemyTargetReached;
        hpModel.onDeath += GameOver;
    }

    private void OnDisable()
    {
        EnemyController.onTargetReached -= OnEnemyTargetReached;
        hpModel.onDeath -= GameOver;
    }

    public void OnEnemyTargetReached(GameObject enemy)
    {
        hpModel.ChangeHP(1);
    }

    void GameOver()
    {
        
    }

}
