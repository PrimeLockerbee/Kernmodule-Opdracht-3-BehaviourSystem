using System.Collections.Generic;

public class BTWeaponSeeker : BTObjectSeeker<Weapon>
{
    public override string s_DisplayName => "Seeking a Weapon";

    public BTWeaponSeeker(Blackboard _blackboard, float _maxDistance) : base(_blackboard, _maxDistance)
    {
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void SetupEvaluators()
    {
        uv_Evaluators = new List<UtilityEval>
        {
            new DistanceToTargetEvaluator(b_BlackBoard, mf_MaxDistance),
            new DamageEvaluator(b_BlackBoard)
        };
    }
}
