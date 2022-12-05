using UnityEngine;

public class BTAttack : BTBaseNode
{
    public override string s_DisplayName => "Attacking";

    private IsHealthUser ihu_PlayerTarget;
    private Transform t_ControllerTransform;

    private int i_Damage;

    public BTAttack(Blackboard _blackboard, GameObject _target, float _damage) : base(_blackboard)
    {
        ihu_PlayerTarget = _target.GetComponent<IsHealthUser>();
        t_ControllerTransform = _blackboard.Get<Transform>("ControllerTransform");
    }

    public override TaskStatus OnUpdate()
    {
        if(ihu_PlayerTarget == null)
        {
            return TaskStatus.Failed;
        }

        ((MonoBehaviour)ihu_PlayerTarget).gameObject.GetComponent<Player>().TakeDamage(t_ControllerTransform.gameObject, i_Damage);
        return TaskStatus.Success;
    }
}
