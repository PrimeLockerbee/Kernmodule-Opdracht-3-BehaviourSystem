using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWait : BTBaseNode
{
    public override string displayName => "Waiting";
    private float waitTime;
    private float timer;

    public BTWait(Blackboard _blackboard, float _waitTime) : base(_blackboard)
    {
        waitTime = _waitTime;
        timer = _waitTime;
    }

    public override TaskStatus OnExit()
    {
        timer = waitTime;
        return TaskStatus.Success;
    }

    public override TaskStatus OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
