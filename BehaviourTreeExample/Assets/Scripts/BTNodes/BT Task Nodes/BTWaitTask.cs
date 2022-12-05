using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWaitTask : BTBaseNode
{
    public override string s_DisplayName => "Waiting";
    private float f_WaitTime;
    private float f_Timer;

    public BTWaitTask(Blackboard _blackboard, float _waitTime) : base(_blackboard)
    {
        f_WaitTime = _waitTime;
        f_Timer = _waitTime;
    }

    public override TaskStatus OnEnter()
    {
        return TaskStatus.Success;
    }

    public override TaskStatus OnExit()
    {
        f_Timer = f_WaitTime;
        return TaskStatus.Success;
    }

    public override TaskStatus OnUpdate()
    {
        f_Timer -= Time.deltaTime;
        if (f_Timer <= 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
