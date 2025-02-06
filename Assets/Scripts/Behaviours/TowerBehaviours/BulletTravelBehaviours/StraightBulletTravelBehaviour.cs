using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletTravelBehaviour : AbstractBulletTravelBehaviour
{
    Vector3 targetPosition = Vector3.zero;

    public override void TravelTowardsTarget(GameObject target, float travelSpeed)
    {
        if(targetPosition == Vector3.zero)
        {
            targetPosition = target.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, travelSpeed * Time.deltaTime);

        if (isTargetReached())
        {
            onTargetReached.Invoke();
        }
    }

    bool isTargetReached()
    {
        float dist = Vector3.Distance(transform.position, targetPosition);

        return dist < 0.1f;
    }
}
