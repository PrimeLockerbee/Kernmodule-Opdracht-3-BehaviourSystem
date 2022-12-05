public class BTWeaponTaken : BTCondition<bool>
{
    public Guard g_Guard;

    public BTWeaponTaken(Blackboard _blackboard, Guard _guard) : base(_blackboard)
    {
        g_Guard = _guard;
    }

    protected override bool GetComparedValue()
    {
        return g_Guard.w_Weapon != null;
    }
}
