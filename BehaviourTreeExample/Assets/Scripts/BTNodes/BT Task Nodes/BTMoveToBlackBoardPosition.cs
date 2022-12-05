using UnityEngine;
using UnityEngine.AI;

public class BTMoveToBlackBoardPosition : BTBaseNode
{
    public override string s_DisplayName => "Moving to bb Position";
    private NavMeshAgent nma_Agent;
    private Transform t_ControllerTransform;

    private Vector3 v_TargetPos;
    private float f_MinDistance;
    private float f_DistanceToDestination;
    private string s_WaypointBlackboardID;

    public BTMoveToBlackBoardPosition(Blackboard _blackboard, string _waypointBlackboardID, float _minDistance) : base(_blackboard)
    {
        f_MinDistance = _minDistance;
        s_WaypointBlackboardID = _waypointBlackboardID;

        GetBlackboardValues();
    }

    public override TaskStatus OnEnter()
    {
        if (b_BlackBoard.Contains(s_WaypointBlackboardID))
        {
            v_TargetPos = b_BlackBoard.Get<Vector3>(s_WaypointBlackboardID);

            nma_Agent.enabled = true;
            nma_Agent.SetDestination(v_TargetPos);

            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
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

    private void GetBlackboardValues()
    {
        t_ControllerTransform = b_BlackBoard.Get<Transform>("ControllerTransform");
        nma_Agent = b_BlackBoard.Get<NavMeshAgent>("Agent");
    }
}
