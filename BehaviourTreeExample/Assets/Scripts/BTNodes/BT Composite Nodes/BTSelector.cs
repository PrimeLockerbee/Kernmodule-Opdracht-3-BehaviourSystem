using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTComposite
{
    public BTSelector(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard, _children)
    {
    }

    public override TaskStatus OnUpdate()
    {
        for (i_CurrentIndex = 0; i_CurrentIndex < bbn_Children.Length; i_CurrentIndex++)
        {
            BTBaseNode node = bbn_Children[i_CurrentIndex];
            switch (node.Evaluate())
            {
                case TaskStatus.Running:
                    return TaskStatus.Running;

                case TaskStatus.Success:
                    i_CurrentIndex = 0;
                    return TaskStatus.Success;

                case TaskStatus.Failed:
                    break;
            }
        }

        i_CurrentIndex = 0;
        return TaskStatus.Failed;
    }
}
