using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestTargetingBehaviour : TargetingBehaviour
{
    public override void setTargets(List<GameObject> targets)
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach(GameObject enemy in GameController.instance.enemies)
        {
            float dist = Vector3.Magnitude(enemy.transform.position - transform.position);
            if (dist < closestDistance && dist <= range)
            {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && !targets.Contains(closestEnemy)) {
            targets.Insert(0, closestEnemy);
        }
    }
}
