using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClosestTargetingBehaviour : TargetingBehaviour
{
    [SerializeField] LayerMask EnemyLayer;

    public override void RemoveTarget(GameObject target)
    {
        targets.Remove(target);
    }

    public override void GetTargets()
    {
        RemoveDeadTargets();

        RemoveOutOfRangeTargets();

        GameObject closestTarget = GetClosestTarget();
        AddTarget(closestTarget);
    }

    GameObject GetClosestTarget()
    {
        List<Collider> enemiesInRange = Physics.OverlapSphere(transform.position, range, EnemyLayer).ToList();

        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider enemyCollider in enemiesInRange)
        {
            float dist = Vector3.Distance(enemyCollider.transform.position, transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestTarget = enemyCollider.gameObject;
            }
        }

        return closestTarget;
    }

    void AddTarget(GameObject target)
    {
        if (target == null || targets.Contains(target))
        {
            return;
        }

        if (targets.Count > 0)
        {
            targets[0] = target;
        }
        else
        {
            targets.Insert(0, target);
        }
        
    }

    void RemoveOutOfRangeTargets()
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (!IsInRange(targets[i].transform.position))
            {
                targets.RemoveAt(i);
            }
        }
    }

    void RemoveDeadTargets()
    {
        for(int i = targets.Count - 1; i >= 0; i--)
        {
            if (!EnemyController.Enemies.Contains(targets[i]))
            {
                RemoveTarget(targets[i]);
            }
        }
    }
}
