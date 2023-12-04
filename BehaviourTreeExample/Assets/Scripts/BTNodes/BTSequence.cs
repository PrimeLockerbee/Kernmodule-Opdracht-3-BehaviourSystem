using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTComposite
{
    public BTSequence(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard, _children)
    {
    }

    public override TaskStatus OnEnter()
    {
        currentIndex = 0;
        return TaskStatus.Success;
    }

    public override TaskStatus OnUpdate()
    {
        for (; currentIndex < children.Length; currentIndex++)
        {
            BTBaseNode node = children[currentIndex];
            switch (node.Evaluate())
            {
                case TaskStatus.Running:
                    return TaskStatus.Running;

                case TaskStatus.Failed:
                    currentIndex = 0;
                    return TaskStatus.Failed;

                case TaskStatus.Success:
                    break;
            }
        }

        currentIndex = 0;
        return TaskStatus.Success;
    }

    public override TaskStatus OnExit()
    {
        currentIndex = 0;

        return TaskStatus.Success;
    }

    public override void Abort()
    {
        base.Abort();

        currentIndex = 0;

        foreach (BTBaseNode child in children)
        {
            child.Abort();
        }
    }
}
