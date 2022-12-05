using UnityEngine;

public class BTThrowObjectAtTarget : BTBaseNode
{
    public override string s_DisplayName => "Throwing the Smokebomb";

    private GameObject go_ObjectPrefab;
    private Transform t_Target;

    public BTThrowObjectAtTarget(Blackboard _blackboard, GameObject _objectPrefab, Transform _target) : base(_blackboard)
    {
        go_ObjectPrefab = _objectPrefab;
        t_Target = _target;
    }

    public override TaskStatus OnEnter()
    {
        GameObject.Instantiate(go_ObjectPrefab, t_Target.position, Quaternion.identity);
        return base.OnEnter();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
