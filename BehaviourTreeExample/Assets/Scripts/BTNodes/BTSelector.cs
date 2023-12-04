public class BTSelector : BTComposite
{
    public BTSelector(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard, _children)
    {
    }

    public override TaskStatus OnUpdate()
    {
        for (currentIndex = 0; currentIndex < children.Length; currentIndex++)
        {
            BTBaseNode node = children[currentIndex];
            switch (node.Evaluate())
            {
                case TaskStatus.Running:
                    AbortUpcomingNodes();
                    currentIndex = 0;
                    return TaskStatus.Running;

                // Succeed whenever one of the children succeeds
                case TaskStatus.Success:
                    AbortUpcomingNodes();
                    currentIndex = 0;
                    return TaskStatus.Success;

                // Move to next node when current node fails
                case TaskStatus.Failed:
                    break;
            }
        }

        // Fail when all children fail
        currentIndex = 0;
        return TaskStatus.Failed;
    }

    public override void Abort()
    {

    }

    private void AbortUpcomingNodes()
    {
        currentIndex++;
        for (; currentIndex < children.Length; currentIndex++)
        {
            BTBaseNode node = children[currentIndex];
            node.Abort();
        }
    }
}
