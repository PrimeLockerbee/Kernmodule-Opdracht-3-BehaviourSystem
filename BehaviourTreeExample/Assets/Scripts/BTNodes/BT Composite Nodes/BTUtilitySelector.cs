using System.Linq;

public class BTUtilitySelector : BTSelector
{
    public BTUtilitySelector(Blackboard _blackboard, params BTBaseNode[] _children) : base(_blackboard, _children)
    {
    }

    public override TaskStatus OnUpdate()
    {
        bbn_Children = bbn_Children.OrderByDescending(node => node.f_UtilityScore).ToArray();
        return base.OnUpdate();
    }
}
