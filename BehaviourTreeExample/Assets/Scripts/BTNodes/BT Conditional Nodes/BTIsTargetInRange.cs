using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIsTargetInRange : BTCondition<float>
{
    private Transform t_ControllerTransform;
    private Transform t_Target;

    public BTIsTargetInRange(Blackboard _blackBoard, Transform _target, float _range) : base(_blackBoard)
    {
        t_Target = _target;
        t_ControllerTransform = b_BlackBoard.Get<Transform>("ControllerTransform");

        sp_Condition = (float distanceToTarget) => distanceToTarget < _range;
    }

    protected override float GetComparedValue()
    {
        return Vector3.Distance(t_Target.position, t_ControllerTransform.position);
    }
}
