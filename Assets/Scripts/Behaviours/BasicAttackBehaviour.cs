using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicAttackBehaviour : AttackBehaviour
{
    float timePassed;
    [SerializeField] GameObject bulletPrefab;

    private void Start()
    {
        timePassed = attackSpeed;
    }

    public override void Attack(GameObject target)
    {
        timePassed += Time.deltaTime;
        if(timePassed >= attackSpeed)
        {
            timePassed = 0;
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletController bulletController = newBullet.GetComponent<BulletController>();
            bulletController.onHit += TargetHit;
            bulletController.Shoot(target);
        }
    }

    void TargetHit(GameObject target)
    {
        HPModel targetHPModel = target.GetComponent<HPModel>();

        if (targetHPModel != null) { 
            targetHPModel.LoseHP(attack);
        }
    }
}
