using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] AttackBehaviour attackBehaviour;
    [SerializeField] TargetingBehaviour targetingBehaviour;

    [Header("Properties")]
    public int cost = 1;

    [HideInInspector] public bool isPlaced = false;

    public System.Action onAttack;

    void Awake()
    {
        attackBehaviour = GetComponent<AttackBehaviour>();
        targetingBehaviour = GetComponent<TargetingBehaviour>();
    }

    private void FixedUpdate()
    {
        if (!isPlaced) {
            return;
        }

        GetTargets();

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

    public void GetTargets()
    {
        if (targetingBehaviour != null)
        {
            targetingBehaviour.GetTargets();
        }
    }
}
