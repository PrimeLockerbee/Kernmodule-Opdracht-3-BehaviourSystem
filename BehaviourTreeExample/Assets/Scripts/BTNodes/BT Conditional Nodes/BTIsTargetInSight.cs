using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIsTargetInSight : BTCondition<bool>
{
    public override string s_DisplayName => "Check if Target is in Sight";

    private RaycastHit rch_HitInSight;
    private Transform t_Target;
    private Transform t_ControllerTransform;
    private float f_InSightRange;

    public BTIsTargetInSight(Blackboard _blackboard, Transform _target, float _inSightRange) : base(_blackboard)
    {
        t_Target = _target;
        f_InSightRange = _inSightRange;
        t_ControllerTransform = b_BlackBoard.Get<Transform>("ControllerTransform");

        sp_Condition = (bool isInSight) => isInSight;
    }

    // Returns true when target is within angle range and distance is not obstructed by wall
    protected override bool GetComparedValue()
    {
        Vector3 directionToOther = (t_Target.position - t_ControllerTransform.position).normalized;
        if (Vector3.Dot(t_ControllerTransform.forward, directionToOther) < f_InSightRange)
        {
            return false;
        }

        if (Physics.Raycast(t_ControllerTransform.position, directionToOther, out rch_HitInSight, 50))
        {
            if (rch_HitInSight.collider.gameObject.transform != t_Target)
            {
                return false;
            }
        }

        return true;
    }
}
