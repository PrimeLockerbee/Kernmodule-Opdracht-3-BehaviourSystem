using UnityEngine;

public abstract class BTCondition<T> : BTBaseNode
{
    public override string displayName => "";
    protected System.Predicate<T> condition;
    protected T comparedValue;
    private int field;

    public BTCondition(Blackboard _blackBoard) : base(_blackBoard)
    {
    }

    public override TaskStatus OnUpdate()
    {
        comparedValue = GetComparedValue();
        bool? conditionResultNullable = condition?.Invoke(comparedValue);
        bool conditionResult = conditionResultNullable ?? false;

        if (conditionResult)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
    }

    protected abstract T GetComparedValue();
}
