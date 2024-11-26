using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] AttackBehaviour attackBehaviour;
    [SerializeField] TargetingBehaviour targetingBehaviour;

    public List<GameObject> targets;

    void Awake()
    {
        attackBehaviour = GetComponent<AttackBehaviour>();
        targetingBehaviour = GetComponent<TargetingBehaviour>();

        targets = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        SetTargets();

        //REMOVE TARGETS FROM TARGET LIST ON ENEMY DEATH
        foreach (GameObject target in targets) {
            if (target != null)
            {
               Attack(target);
            }
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
            targetingBehaviour.setTargets(targets);
        }
    }

    public void RemoveTarget(GameObject target)
    {
        targets.Remove(target);
    }
}
