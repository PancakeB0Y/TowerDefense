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
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float health = 100f;
    [SerializeField] float attack;
    [SerializeField] float defense;

    [Header("Events")]
    public UnityEvent<GameObject> onTargetReached;
    public UnityEvent<GameObject> onDeath;

    void Awake()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
    }

    private void Start()
    {
        onTargetReached.AddListener(GameController.instance.OnEnemyTargetReached);
        onDeath.AddListener(GameController.instance.OnEnemyDeath);
    }

    private void FixedUpdate()
    {
        if (moveBehaviour.IsTargetReached())
        {
            onTargetReached?.Invoke(gameObject);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        moveBehaviour.SetTargetPosition(targetPosition);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0){ 
            onDeath?.Invoke(gameObject);
        }
    }
}
