using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInvert : BTDecorator
{
    public override string s_DisplayName => bbn_Child.s_DisplayName;

    public BTInvert(Blackboard _blackBoard, BTBaseNode _node) : base(_blackBoard, _node)
    {
    }

    public override TaskStatus OnUpdate()
    {
        switch (bbn_Child.Evaluate())
        {
            case TaskStatus.Success: return TaskStatus.Failed;
            case TaskStatus.Failed: return TaskStatus.Success;
            case TaskStatus.Running: return TaskStatus.Running;
            default: return TaskStatus.Success;
        }
    }
}
