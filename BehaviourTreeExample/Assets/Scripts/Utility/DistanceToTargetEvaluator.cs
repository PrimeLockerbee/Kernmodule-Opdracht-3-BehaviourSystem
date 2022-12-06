using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DistanceToTargetEvaluator : UtilityEval
{
    private Transform t_Target;
    private Transform t_ControllerTransform;
    private NavMeshAgent nma_Agent;
    private float f_DistanceToTarget;
    private float f_MaxDistance;

    public DistanceToTargetEvaluator(Blackboard _blackBoard, float _maxDistance) : base(_blackBoard)
    {
        f_MaxDistance = _maxDistance;
        t_ControllerTransform = _blackBoard.Get<Transform>("ControllerTransform");
        nma_Agent = _blackBoard.Get<NavMeshAgent>("Agent");
    }

    protected override AnimationCurve GetCurve()
    {
        return bb_BlackBoard.Get<AnimationCurve>("DistanceCurve");
    }

    protected override float GetMinValue()
    {
        return 0;
    }

    protected override float GetMaxValue()
    {
        return f_MaxDistance;
    }

    protected override float GetValue()
    {
        Vector3 objectPos = bb_BlackBoard.Get<MonoBehaviour>("Current Search Item").transform.position;
        float distanceToObject = Vector3.Distance(t_ControllerTransform.position, objectPos);

        return distanceToObject;
    }
}
