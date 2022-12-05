public abstract class BTDecorator : BTBaseNode
{
    protected BTBaseNode bbn_Child;

    public BTDecorator(Blackboard _blackBoard, BTBaseNode _node) : base(_blackBoard)
    {
        bbn_Child = _node;
    }
}
