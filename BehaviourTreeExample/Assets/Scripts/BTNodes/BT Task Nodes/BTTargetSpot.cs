
public class BTTargetSpot : BTBaseNode
{
    public override string s_DisplayName => "Spotted a Target";

    private IsSpotable is_Spottable;
    private bool b_Spotted;

    public BTTargetSpot(Blackboard _blackBoard, IsSpotable _spottable, bool _spotted) : base(_blackBoard)
    {
        is_Spottable = _spottable;
        b_Spotted = _spotted;
    }

    public override TaskStatus OnEnter()
    {
        if (is_Spottable == null) return TaskStatus.Failed;

        is_Spottable.b_IsSpotted = b_Spotted;
        return base.OnEnter();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
