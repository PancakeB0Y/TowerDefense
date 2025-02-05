using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] protected float attack = 1f;
    [SerializeField] protected float attackSpeed = 1f;
    [SerializeField] protected float bulletSpeed = 10f;

    public abstract void Attack(GameObject target);
}
