using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class MoveBehaviour : MonoBehaviour
{
    [SerializeField] protected float targetRange = 0.2f;
    Vector3 targetPosition;

    public virtual void SetTargetPosition(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    public abstract bool IsTargetReached();

    public abstract void SetSpeed(float speed);
}
