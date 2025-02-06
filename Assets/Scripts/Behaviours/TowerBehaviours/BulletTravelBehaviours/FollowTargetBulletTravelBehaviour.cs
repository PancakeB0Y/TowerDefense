using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetBulletTravelBehaviour : AbstractBulletTravelBehaviour
{
    public override void TravelTowardsTarget(GameObject target, float travelSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, travelSpeed * Time.deltaTime);
    }

}
