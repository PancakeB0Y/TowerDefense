using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackBehaviour : AttackBehaviour
{
    private IEnumerator coroutine;

    public override void Attack(GameObject target)
    {
        
        EnemyController enemyController = target.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            enemyController.TakeDamage(attack);
        }
    }

    private IEnumerator AttackCoroutine(EnemyController enemyController)
    {
        while (true)
        {
            enemyController.TakeDamage(attack);

            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
