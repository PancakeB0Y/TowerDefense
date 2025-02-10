using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMoveBehaviour : MoveBehaviour
{
    [SerializeField] NavMeshAgent agent;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void SetTargetPosition(Vector3 targetPosition)
    {
        base.SetTargetPosition(targetPosition);

        if (agent != null)
        {
            agent.SetDestination(targetPosition);
        }
    }

    public override bool IsTargetReached()
    {
        float dist = agent.remainingDistance;

        if (dist!=0 && dist != Mathf.Infinity && dist < targetRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void SetSpeed(float speed)
    {
        agent.speed = speed;
        agent.acceleration = speed * 2;
    }
}
