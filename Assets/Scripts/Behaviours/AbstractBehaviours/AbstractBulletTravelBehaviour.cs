using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBulletTravelBehaviour : MonoBehaviour
{

    public System.Action onTargetReached;
    public abstract void TravelTowardsTarget(GameObject target, float travelSpeed);
}
