using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPivot : MonoBehaviour
{
    public void RotateTowardsTarget(Transform target)
    {
        transform.LookAt(target);
    }
}
