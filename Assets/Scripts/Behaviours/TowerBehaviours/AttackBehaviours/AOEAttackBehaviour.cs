using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AOEAttackBehaviour : AttackBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject explosionPrefab;

    [Header("Properties")]
    [SerializeField] float AOERadius = 3;
    [SerializeField] LayerMask TargetLayer;

    float timePassed;
    HeadPivot headPivot;

    Vector3 curTarget = Vector3.zero;


    private void Start()
    {
        timePassed = attackSpeed;
        headPivot = GetComponentInChildren<HeadPivot>();

        BulletController bulletController = bulletPrefab.GetComponent<BulletController>();

        if(bulletController != null)
        {
            bulletController.SetBulletSpeed(bulletSpeed);
        }

        explosionPrefab = Instantiate(explosionPrefab);
        explosionPrefab.SetActive(false);
        explosionPrefab.transform.localScale = new Vector3(AOERadius, AOERadius, AOERadius);
    }

    public override void Attack(GameObject target)
    {
        timePassed += Time.deltaTime;
        headPivot.RotateTowardsTarget(target.transform);
        if(timePassed >= attackSpeed)
        {
            timePassed = 0;

            explosionPrefab.SetActive(false);

            curTarget = target.transform.position;

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BulletController bulletController = newBullet.GetComponent<BulletController>();
            bulletController.onHit += TargetHit;
            bulletController.Shoot(target);
        }
    }

    void TargetHit()
    {
        if (curTarget == Vector3.zero)
        {
            return;
        }

        explosionPrefab.transform.position = curTarget;
        explosionPrefab.SetActive(true);

        List<Collider> enemiesHit = Physics.OverlapSphere(curTarget, AOERadius, TargetLayer).ToList();

        for (int i = 0; i < enemiesHit.Count; i++) {
            GameObject curEnemy = enemiesHit[i].gameObject;

            HPModel targetHPModel = curEnemy.GetComponent<HPModel>();

            if (targetHPModel != null)
            {
                targetHPModel.LoseHP(attack);
            }
        }

        curTarget = Vector3.zero;
    }
}
