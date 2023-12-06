using System;

public class BTCustomCondition : BTBaseNode
{
    public delegate bool conditionCheck();
    public override string displayName => "Condition";

    private Func<bool> condition;

    public BTCustomCondition(Blackboard _blackboard, Func<bool> _condition) : base(_blackboard)
    {
        condition = _condition;
    }

    public override TaskStatus OnUpdate()
    {
        if ((bool)condition?.Invoke())
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failed;
    }
}
