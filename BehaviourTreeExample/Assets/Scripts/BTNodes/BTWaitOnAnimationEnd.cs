using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWaitOnAnimationEnd : BTBaseNode
{
    public override string displayName => "Waiting on Animation End";

    private Animator animator;
    private BTWait waitNode;

    public BTWaitOnAnimationEnd(Blackboard _blackboard) : base(_blackboard)
    {
        animator = blackboard.Get<Animator>("Animator");
    }

    public override TaskStatus OnEnter()
    {
        float currentClipLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        waitNode = new BTWait(blackboard, currentClipLength);
        return waitNode.OnEnter();
    }

    public override TaskStatus OnUpdate()
    {
        return waitNode.OnUpdate();
    }

    public override TaskStatus OnExit()
    {
        return waitNode.OnExit();
    }
}
