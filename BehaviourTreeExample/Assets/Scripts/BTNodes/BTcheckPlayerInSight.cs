using UnityEngine;
using UnityEngine.AI;

public class BTcheckPlayerInSight : BTCondition<bool>
{
    public override string displayName => "Check if Target in Sight";

    private RaycastHit hitInSight;
    private Transform target;
    private Transform controllerTransform;
    private float inSightRange;
    private Collider targetCollider;

    private const float RaycastMaxDistance = 50f; // Replace 50 with a meaningful name

    public BTcheckPlayerInSight(Blackboard blackboard, Transform target, float inSightRange) : base(blackboard)
    {
        this.target = target;
        this.inSightRange = inSightRange;
        controllerTransform = blackboard.Get<Transform>("ControllerTransform");
        targetCollider = blackboard.Get<Collider>("TargetCollider");

        condition = (bool isInSight) => isInSight;
    }

    protected override bool GetComparedValue()
    {
        Vector3 directionToOther = (target.position - controllerTransform.position).normalized;

        if (Vector3.Dot(controllerTransform.forward, directionToOther) < inSightRange)
        {
            return false;
        }

        if (Physics.Raycast(controllerTransform.position, directionToOther, out hitInSight, RaycastMaxDistance))
        {
            if (hitInSight.collider.gameObject.transform != target)
            {
                Vector3 boxExtends = targetCollider.bounds.extents;

                if (Physics.BoxCast(controllerTransform.position, directionToOther, boxExtends, out hitInSight, Quaternion.identity, RaycastMaxDistance))
                {
                    if (hitInSight.collider.gameObject.transform != target)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
