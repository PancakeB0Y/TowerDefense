using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class MoveBehaviour : MonoBehaviour
{
    protected float targetRange = 0.2f;
    Vector3 targetPosition;

    public virtual void SetTargetPosition(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    public virtual bool IsTargetReached()
    {
        float dist = Vector3.Magnitude(targetPosition - transform.position);
        if (dist < targetRange) {
            return true;
        }
        else
        {
            return false;
        }
    }
}
