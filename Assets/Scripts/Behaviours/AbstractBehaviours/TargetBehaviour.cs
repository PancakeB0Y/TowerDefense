using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetingBehaviour : MonoBehaviour
{
    [Header("Targeting Properties")]
    [SerializeField] protected float range = 10f;
    [SerializeField] protected int targetsCount = 1;

    [Header("Current Targets")]
    [SerializeField] public List<GameObject> targets = new List<GameObject>();

    public float GetRange()
    {
        return range;
    }

    public abstract void RemoveTarget(GameObject target);
    public abstract void GetTargets();

    protected bool IsInRange(Vector3 objectPos)
    {
        Vector3 pos1 = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 pos2 = new Vector3(objectPos.x, 0, objectPos.z);
        
        float dist = Vector3.Distance(pos1, pos2);
        return dist <= range;
    }
}
