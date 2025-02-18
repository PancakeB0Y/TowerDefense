using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseController : MonoBehaviour
{
    public static BaseController instance { get; private set; }

    [Header("Debug field")]
    [SerializeField] HPModel hpModel;

    public System.Action onBaseDeath;

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

        hpModel = GetComponent<HPModel>();
    }

    private void OnEnable()
    {
        EnemyController.onTargetReached += OnEnemyTargetReached;
        hpModel.onDeath += BaseDeath;
    }

    private void OnDisable()
    {
        EnemyController.onTargetReached -= OnEnemyTargetReached;
        hpModel.onDeath -= BaseDeath;
    }

    public void OnEnemyTargetReached(GameObject enemy)
    {
        hpModel.LoseHP(1);
    }

    void BaseDeath()
    {
        onBaseDeath?.Invoke();
        //Play base death animation
    }
}
