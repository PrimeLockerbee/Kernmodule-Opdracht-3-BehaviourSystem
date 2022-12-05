using UnityEngine;

public class DamageEvaluator : UtilityEval
{
    public DamageEvaluator(Blackboard _blackBoard) : base(_blackBoard)
    {
    }

    protected override AnimationCurve GetCurve()
    {
        return bb_BlackBoard.Get<AnimationCurve>("DamageCurve");
    }

    protected override float GetMinValue()
    {
        return 0;
    }

    protected override float GetMaxValue()
    {
        return 100;
    }

    protected override float GetValue()
    {
        // More Modular: IDamager
        Weapon weapon = bb_BlackBoard.Get<Weapon>("Current Search Item");
        if (weapon != null)
        {
            return weapon.damage;
        }
        return 0;
    }
}
