using UnityEngine;
using UnityEngine.AI;

public class BTTargetFollow : BTBaseNode
{
    public override string s_DisplayName => "Following the Target";

    private Transform t_ControllerTransform;
    private Transform t_Target;
    private NavMeshAgent nma_Agent;
    private float f_MinDistance;
    private float f_DistanceToTarget;
    private bool b_ShouldKeepDistance;

    public BTTargetFollow(Blackboard _blackboard, Transform _target, float _minDistance) : base(_blackboard)
    {
        t_Target = _target;

        f_MinDistance = _minDistance;
        t_ControllerTransform = b_BlackBoard.Get<Transform>("ControllerTransform");
        nma_Agent = b_BlackBoard.Get<NavMeshAgent>("Agent");
    }

    public override TaskStatus OnUpdate()
    {
        nma_Agent.enabled = true;
        nma_Agent.SetDestination(t_Target.position);

        f_DistanceToTarget = Vector3.Distance(t_ControllerTransform.position, t_Target.position);
        if (f_DistanceToTarget > f_MinDistance)
        {
            return TaskStatus.Running;
        }
        else
        {
            nma_Agent.enabled = false;
            return TaskStatus.Success;
        }
    }
}
