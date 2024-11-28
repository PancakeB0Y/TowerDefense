using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackBehaviour : AttackBehaviour
{
    float timePassed;

    private void Start()
    {
        timePassed = attackSpeed;
    }

    public override void Attack(GameObject target)
    {
        timePassed += Time.deltaTime;

        EnemyController enemyController = target.GetComponent<EnemyController>();

        if (enemyController != null && timePassed >= attackSpeed)
        {
            enemyController.GetHit(attack);
            timePassed = 0;
        }
    }
}
