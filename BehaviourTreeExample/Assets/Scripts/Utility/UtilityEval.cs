using UnityEngine;

public abstract class UtilityEval
{
    protected Blackboard bb_BlackBoard;
    protected AnimationCurve ac_Curve;

    public UtilityEval(Blackboard _blackboard)
    {
        bb_BlackBoard = _blackboard;
    }

    public virtual float GetNormalizedScore()
    {
        float normalizedvalue = Mathf.InverseLerp(GetMinValue(), GetMaxValue(), GetValue());
        return Mathf.Clamp01(GetCurve().Evaluate(normalizedvalue));
    }

    protected abstract float GetValue();
    protected abstract float GetMinValue();
    protected abstract float GetMaxValue();
    protected abstract AnimationCurve GetCurve();
}
