using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

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

        agent.SetDestination(targetPosition);
    }

    public override bool IsTargetReached()
    {
        float dist = agent.remainingDistance;
        if (!agent.pathPending && dist != Mathf.Infinity && dist < targetRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
