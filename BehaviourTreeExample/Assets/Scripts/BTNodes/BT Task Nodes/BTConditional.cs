using System;

public class BTConditional : BTBaseNode
{
    public delegate bool conditionCheck();
    public override string s_DisplayName => "Condition";

    private Func<bool> b_Condition;

    public BTConditional(Blackboard _blackboard, Func<bool> _condition) : base(_blackboard)
    {
        b_Condition = _condition;
    }

    public override TaskStatus OnUpdate()
    {
        if ((bool)b_Condition?.Invoke())
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failed;
    }
}
