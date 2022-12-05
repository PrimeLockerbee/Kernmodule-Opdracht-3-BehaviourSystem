public class BTPickupWeapon : BTBaseNode
{
    public override string s_DisplayName => "Pickup the Weapon";

    private Guard g_GuardBase;

    public BTPickupWeapon(Blackboard _blackboard) : base(_blackboard)
    {
        g_GuardBase = b_BlackBoard.Get<Guard>("Base");
    }

    public override TaskStatus OnEnter()
    {
        Weapon weapon = b_BlackBoard.Get<Weapon>("BestWeapon");
        g_GuardBase.w_Weapon = weapon;

        if (weapon == null) return TaskStatus.Failed;
        else return TaskStatus.Success;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
