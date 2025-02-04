using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetingBehaviour : MonoBehaviour
{
    [Header("Targeting Properties")]
    public float range = 100f;
    [SerializeField] protected int targetsCount = 1;

    [Header("Current Targets")]
    [SerializeField] public List<GameObject> targets = new List<GameObject>();

    public abstract void RemoveTarget(GameObject target);
    public abstract void GetTargets();

    protected bool IsInRange(Vector3 objectPos)
    {
        float dist = Vector3.Magnitude(objectPos - transform.position);
        return dist <= range;
    }
}
