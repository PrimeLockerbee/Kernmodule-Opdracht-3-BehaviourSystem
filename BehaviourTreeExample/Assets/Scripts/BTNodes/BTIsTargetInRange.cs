using UnityEngine;

public class BTIsTargetInRange : BTCondition<float>
{
    private Transform controllerTransform;
    private Transform target;
    private float range;

    public BTIsTargetInRange(Blackboard _blackBoard, Transform _target, float _range) : base(_blackBoard)
    {
        target = _target;
        controllerTransform = blackboard.Get<Transform>("ControllerTransform");
        range = _range;

        condition = (float distanceToTarget) => { return target != null && distanceToTarget <= range; };
    }

    protected override float GetComparedValue()
    {
        return Vector3.Distance(target.position, controllerTransform.position);
    }

    public override TaskStatus OnUpdate()
    {
        if (condition(GetComparedValue()))
        {
            Debug.Log("Player is in range!");
            return TaskStatus.Success;
        }

        Debug.Log("Player is not in range.");
        return TaskStatus.Failed;
    }
}
