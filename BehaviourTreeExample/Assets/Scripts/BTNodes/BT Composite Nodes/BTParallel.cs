using UnityEngine;

public class BTParallel : BTComposite
{
    public BTParallel(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard, _children)
    {
    }

    public override TaskStatus OnUpdate()
    {
        foreach (BTBaseNode node in bbn_Children)
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
