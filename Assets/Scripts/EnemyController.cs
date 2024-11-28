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
    public static System.Action<float> onHit;

    private void Awake()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
        hpModel = GetComponent<HPModel>();
    }

    private void OnEnable()
    {
        HPModel.onDeath += Die;
    }

    private void OnDisable()
    {
        HPModel.onDeath -= Die;
    }

    private void FixedUpdate()
    {
        if (moveBehaviour.IsTargetReached())
        {
            onTargetReached?.Invoke(gameObject);
            Die(gameObject);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.SetTargetPosition(targetPosition);
        }
    }

    public void GetHit(float damage)
    {
        onHit?.Invoke(damage);
    }

    public void Die(GameObject enemy)
    {
        Destroy(this);
    }
}
