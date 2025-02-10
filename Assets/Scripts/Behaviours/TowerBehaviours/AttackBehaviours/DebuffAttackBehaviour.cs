using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DebuffAttackBehaviour : AttackBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject bulletPrefab;

    [Header("Properties")]
    [SerializeField] float slowAmountPercent = 30;
    [SerializeField] float slowDuration = 3;

    float timePassed;
    HeadPivot headPivot;

    GameObject curTarget;

    private void Start()
    {
        timePassed = attackSpeed;
        headPivot = GetComponentInChildren<HeadPivot>();

        BulletController bulletController = bulletPrefab.GetComponent<BulletController>();

        if(bulletController != null)
        {
            bulletController.SetBulletSpeed(bulletSpeed);
        }
    }

    public override void Attack(GameObject target)
    {
        timePassed += Time.deltaTime;
        headPivot.RotateTowardsTarget(target.transform);
        if(timePassed >= attackSpeed)
        {
            timePassed = 0;

            curTarget = target;

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletController bulletController = newBullet.GetComponent<BulletController>();
            bulletController.onHit += HandleTargetHit;
            bulletController.Shoot(curTarget);
        }
    }

    void HandleTargetHit()
    {
        if (curTarget == null) {
            return;
        }

        EnemyController enemyController = curTarget.GetComponent<EnemyController>();

        if (enemyController != null) {
            enemyController.ApplySlow(slowAmountPercent, slowDuration);
            curTarget = null;
        }
    }
}
