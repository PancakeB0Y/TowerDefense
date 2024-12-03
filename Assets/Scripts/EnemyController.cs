using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MoveBehaviour))]
public class EnemyController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] MoveBehaviour moveBehaviour;

    [Header("Stats")]
    [SerializeField] HPModel hpModel;
    [SerializeField] float attack;
    [SerializeField] float defense;

    [Header("Events")]
    public static System.Action<GameObject> onTargetReached;

    public static List<GameObject> Enemies { get; private set; } = new List<GameObject>();

    private void Awake()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
        hpModel = GetComponent<HPModel>();
    }

    private void OnEnable()
    {
        hpModel.onDeath += Die;
    }

    private void OnDisable()
    {
        hpModel.onDeath -= Die;
    }

    private void FixedUpdate()
    {
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
        Enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public static void AddEnemy(GameObject enemy)
    {
        Enemies.Add(enemy);
    }
}
