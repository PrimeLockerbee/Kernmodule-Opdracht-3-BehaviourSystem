public class BTConditionDecorator : BTBaseNode
{
    public override string s_DisplayName => bbn_ChildNode.s_DisplayName;

    private BTBaseNode bbn_ConditionNode;
    private BTBaseNode bbn_ChildNode;

    public BTConditionDecorator(Blackboard _blackboard, BTBaseNode _conditionNode, BTBaseNode _childNode) : base(_blackboard)
    {
        bbn_ConditionNode = _conditionNode;
        bbn_ChildNode = _childNode;
    }

    public override TaskStatus OnUpdate()
    {
        TaskStatus conditionStatus = bbn_ConditionNode.Evaluate();
        if (conditionStatus == TaskStatus.Success)
        {
            return bbn_ChildNode.Evaluate();
        }

        return conditionStatus;
    }
}
