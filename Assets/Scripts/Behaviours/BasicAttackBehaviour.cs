using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicAttackBehaviour : AttackBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject bulletPrefab;

    float timePassed;
    HeadPivot headPivot;

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
