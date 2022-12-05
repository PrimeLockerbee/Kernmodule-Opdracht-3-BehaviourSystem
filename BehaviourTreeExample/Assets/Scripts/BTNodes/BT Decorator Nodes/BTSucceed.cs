public abstract class BTSucceed : BTDecorator
{
    public override string s_DisplayName => bbn_Child.s_DisplayName;

    public BTSucceed(Blackboard _blackBoard, BTBaseNode _node) : base(_blackBoard, _node)
    {
    }

    public override TaskStatus OnUpdate()
    {
        bbn_Child.Evaluate();
        return TaskStatus.Success;
    }
}
