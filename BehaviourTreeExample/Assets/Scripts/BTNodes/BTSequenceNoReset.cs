using UnityEngine;

public class BTSequenceNoReset : BTSequence
{
    public BTSequenceNoReset(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard, _children)
    {
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

    public override void Abort()
    {
        base.Abort();

        foreach (BTBaseNode child in children)
        {
            child.Abort();
        }
    }
}
