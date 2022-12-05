using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTaskDebug : BTBaseNode
{
    public override string s_DisplayName => "Debugging";
    private string s_Message;

    public BTTaskDebug(Blackboard _blackboard, string _message) : base(_blackboard)
    {
        s_Message = _message;
    }

    public override TaskStatus OnEnter()
    {
        return TaskStatus.Success;
    }
    public override TaskStatus OnExit()
    {
        return TaskStatus.Success;
    }

    public override TaskStatus OnUpdate()
    {
        Debug.Log(s_Message);
        return TaskStatus.Success;
    }
}
