using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetingBehaviour : MonoBehaviour
{
    [SerializeField] protected float range = 100f;
    protected int targetsCount = 1;

    public abstract void setTargets(List<GameObject> targets);
}
