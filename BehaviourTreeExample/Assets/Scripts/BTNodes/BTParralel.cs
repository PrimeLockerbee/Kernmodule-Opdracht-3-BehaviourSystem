using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTParralel : BTComposite
{
    public BTParralel(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard, _children)
    {
    }

    public override TaskStatus OnUpdate()
    {
        foreach (BTBaseNode node in children)
        {
            switch (node.Evaluate())
            {
                case TaskStatus.Running:
                    return TaskStatus.Running;

                case TaskStatus.Failed:
                    return TaskStatus.Failed;

                case TaskStatus.Success:
                    break;
            }
        }

        return TaskStatus.Success;
    }
}
