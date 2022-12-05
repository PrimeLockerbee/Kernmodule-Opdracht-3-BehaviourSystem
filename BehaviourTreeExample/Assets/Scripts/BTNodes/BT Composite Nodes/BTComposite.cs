public abstract class BTComposite : BTBaseNode
{
    public override string s_DisplayName => bbn_Children[i_CurrentIndex].s_DisplayName;
    protected BTBaseNode[] bbn_Children;
    protected int i_CurrentIndex;

    public BTComposite(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard)
    {
        bbn_Children = _children;
    }
}
