using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestTargetingBehaviour : TargetingBehaviour
{
    private void OnEnable()
    {
        HPModel.onDeath += RemoveTarget;
    }

    private void OnDisable()
    {
        HPModel.onDeath -= RemoveTarget;
    }

    public override void RemoveTarget(GameObject target)
    {
        targets.Remove(target);
    }

    public override void SetTargets()
    {
        GameObject closestEnemy = GetClosestTarget();

        AddTarget(closestEnemy);

        RemoveOutOfRangeTargets();
    }

    GameObject GetClosestTarget()
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in GameController.instance.enemies)
        {
            float dist = Vector3.Magnitude(enemy.transform.position - transform.position);
            if (dist < closestDistance && IsInRange(enemy.transform.position))
            {
                closestDistance = dist;
                closestTarget = enemy;
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
}
