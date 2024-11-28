using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class TowerController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] AttackBehaviour attackBehaviour;
    [SerializeField] TargetingBehaviour targetingBehaviour;

    public UnityEvent onAttack;

    void Awake()
    {
        attackBehaviour = GetComponent<AttackBehaviour>();
        targetingBehaviour = GetComponent<TargetingBehaviour>();
    }

    private void FixedUpdate()
    {
        SetTargets();

        for (int i = targetingBehaviour.targets.Count - 1; i >= 0; i--)
        {
            Attack(targetingBehaviour.targets[i]);
        }
    }

    public void Attack(GameObject target)
    {
        if (attackBehaviour != null)
        {
            attackBehaviour.Attack(target);
        }
    }

    public void SetTargets()
    {
        if (targetingBehaviour != null)
        {
            targetingBehaviour.SetTargets();
        }
    }
}
