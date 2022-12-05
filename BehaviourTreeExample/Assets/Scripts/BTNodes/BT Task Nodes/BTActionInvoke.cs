using System;

public class BTActionInvoke : BTBaseNode
{
    public override string s_DisplayName => "Invoked Action";

    private Action a_Action;

    public BTActionInvoke(Blackboard _blackboard, System.Action _action) : base(_blackboard)
    {
        a_Action = _action;
    }

    public override TaskStatus OnEnter()
    {
        a_Action.Invoke();
        return base.OnEnter();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
