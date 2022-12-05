using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTCondition<T> : BTBaseNode
{
    public override string s_DisplayName => "Check the target in range. ";
    protected System.Predicate<T> sp_Condition;
    protected T t_ComparedValue;

    public BTCondition(Blackboard _blackboard) : base(_blackboard)
    {
    }

    public override TaskStatus OnUpdate()
    {
        t_ComparedValue = GetComparedValue();
        bool? conditionResultNullable = sp_Condition?.Invoke(t_ComparedValue);
        bool conditionResult = conditionResultNullable ?? false;

        if(conditionResult)
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