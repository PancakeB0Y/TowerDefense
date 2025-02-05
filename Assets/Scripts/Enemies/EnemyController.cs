using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MoveBehaviour))]
public class EnemyController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] MoveBehaviour moveBehaviour;

    [Header("Stats")]
    [SerializeField] HPModel hpModel;
    [SerializeField] float speed = 7;
    [SerializeField] int money = 0;

    [Header("Events")]
    public static System.Action<GameObject> onTargetReached;

    public static List<GameObject> Enemies { get; private set; } = new List<GameObject>();

    private void Awake()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
        hpModel = GetComponent<HPModel>();
    }

    private void Start()
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.SetSpeed(speed);
        }
    }

    private void OnEnable()
    {
        hpModel.onDeath += Die;
        hpModel.onDeath += DropMoney;
    }

    private void OnDisable()
    {
        hpModel.onDeath -= Die;
        hpModel.onDeath -= DropMoney;
    }

    private void FixedUpdate()
    {
        if(moveBehaviour == null)
        {
            return;
        }

        if (moveBehaviour.IsTargetReached())
        {
            onTargetReached?.Invoke(gameObject);
            Die();
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.SetTargetPosition(targetPosition);
        }
    }

    public void Die()
    {
        //Remove enemy from scene
        Enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    void DropMoney()
    {
        //Get money from enemy death
        MoneyManager.instance.GainMoney(money);
    }

    public static void AddEnemy(GameObject enemy)
    {
        Enemies.Add(enemy);
    }
}
