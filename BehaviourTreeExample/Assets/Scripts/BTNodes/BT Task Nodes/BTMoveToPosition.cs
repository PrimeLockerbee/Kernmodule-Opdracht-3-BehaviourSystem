using UnityEngine;
using UnityEngine.AI;

public class BTMoveToPosition : BTBaseNode
{
    public override string s_DisplayName => "Moving to Position";

    private NavMeshAgent nma_Agent;
    private Transform t_ControllerTransform;

    private Vector3 v_TargetPos;
    private float f_MinDistance;
    private float f_DistanceToDestination;

    public BTMoveToPosition(Blackboard _blackboard, Vector3 _waypoint, float _minDistance) : base(_blackboard)
    {
        v_TargetPos = _waypoint;
        f_MinDistance = _minDistance;

        GetBlackboardValues();
    }

    private void GetBlackboardValues()
    {
        t_ControllerTransform = b_BlackBoard.Get<Transform>("ControllerTransform");
        nma_Agent = b_BlackBoard.Get<NavMeshAgent>("Agent");
    }

    public override TaskStatus OnEnter()
    {
        nma_Agent.enabled = true;
        nma_Agent.SetDestination(v_TargetPos);

        return base.OnEnter();
    }

    public override TaskStatus OnUpdate()
    {
        f_DistanceToDestination = Vector3.Distance(t_ControllerTransform.position, v_TargetPos);
        if (f_DistanceToDestination > f_MinDistance)
        {
            return TaskStatus.Running;
        }

        return TaskStatus.Success;
    }
}
