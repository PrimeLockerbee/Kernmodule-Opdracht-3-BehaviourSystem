using UnityEngine;
using UnityEngine.AI;

public class BTMoveToBlackboardPosition : BTBaseNode
{
    public override string displayName => "Moving to Position";
    private NavMeshAgent agent;
    private Transform controllerTransform;

    private Vector3 targetPos;
    private float minDistance;
    private float distanceToDestination;
    private string waypointBlackboardID;

    public BTMoveToBlackboardPosition(Blackboard _blackboard, string _waypointBlackboardID, float _minDistance) : base(_blackboard)
    {
        minDistance = _minDistance;
        waypointBlackboardID = _waypointBlackboardID;

        GetBlackboardValues();
    }

    public override TaskStatus OnEnter()
    {
        if (blackboard.Contains(waypointBlackboardID))
        {
            targetPos = blackboard.Get<Vector3>(waypointBlackboardID);

            agent.enabled = true;
            agent.isStopped = false;
            agent.SetDestination(targetPos);

            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (agent.destination != targetPos)
        {
            agent.SetDestination(targetPos);
        }

        distanceToDestination = Vector3.Distance(controllerTransform.position, targetPos);
        if (distanceToDestination > minDistance)
        {
            return TaskStatus.Running;
        }

        return TaskStatus.Success;
    }

    public override TaskStatus OnExit()
    {
        agent.SetDestination(controllerTransform.position);
        agent.isStopped = true;
        return TaskStatus.Success;
    }

    private void GetBlackboardValues()
    {
        controllerTransform = blackboard.Get<Transform>("ControllerTransform");
        agent = blackboard.Get<NavMeshAgent>("Agent");
    }
}
