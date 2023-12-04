using UnityEngine;
using UnityEngine.AI;

public class BTMoveToPosition : BTBaseNode
{
    public override string displayName => "Moving to Position";
    private NavMeshAgent agent;
    private Transform controllerTransform;

    private Vector3 targetPos;
    private float minDistance;
    private float distanceToDestination;

    public BTMoveToPosition(Blackboard _blackboard, Vector3 _waypoint, float _minDistance) : base(_blackboard)
    {
        targetPos = _waypoint;
        minDistance = _minDistance;

        GetBlackboardValues();
    }

    private void GetBlackboardValues()
    {
        controllerTransform = blackboard.Get<Transform>("ControllerTransform");
        agent = blackboard.Get<NavMeshAgent>("Agent");
    }

    public override TaskStatus OnEnter()
    {
        agent.enabled = true;
        agent.isStopped = false;
        agent.SetDestination(targetPos);

        return TaskStatus.Success;
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
}
