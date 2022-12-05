public class BTSequence : BTComposite
{
    public BTSequence(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard, _children)
    {
    }

    public override TaskStatus OnUpdate()
    {
        for (; i_CurrentIndex < bbn_Children.Length; i_CurrentIndex++)
        {
            BTBaseNode node = bbn_Children[i_CurrentIndex];
            switch (node.Evaluate())
            {
                case TaskStatus.Running:
                    return TaskStatus.Running;

                case TaskStatus.Failed:
                    i_CurrentIndex = 0;
                    return TaskStatus.Failed;

                case TaskStatus.Success:
                    break;
            }
        }

        i_CurrentIndex = 0;
        return TaskStatus.Success;
    }
}
