using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] protected float attack = 5f;
    [SerializeField] protected float attackSpeed = 0.3f;
    [SerializeField] protected float bulletSpeed = 20f;
    public abstract void Attack(GameObject target);

    public float GetAttack()
    {
        return attack;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
