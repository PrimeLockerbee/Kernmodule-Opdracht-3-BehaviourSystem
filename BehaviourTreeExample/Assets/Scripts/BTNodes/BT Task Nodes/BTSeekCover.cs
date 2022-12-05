using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSeekCover : BTObjectSeeker<Cover>
{
    public override string s_DisplayName => "Seek Cover";

    private GameObject go_CoverGameObject;
    private float f_InSightRange;
    private float f_MaxCoverDistance;

    private List<Guard> l_Guards = new List<Guard>();
    private Guard g_ClosestGuard;

    public BTSeekCover(Blackboard _blackboard, float _maxDistance, float _inSightRange) : base(_blackboard, _maxDistance)
    {
        f_InSightRange = _inSightRange;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void SetupEvaluators()
    {
        uv_Evaluators = new List<UtilityEval>
        {
            new DistanceToTargetEvaluator(b_BlackBoard, mf_MaxDistance)
        };
    }
}
