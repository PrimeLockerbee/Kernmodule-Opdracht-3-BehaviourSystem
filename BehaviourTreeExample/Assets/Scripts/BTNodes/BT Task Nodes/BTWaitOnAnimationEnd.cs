using UnityEngine;

public class BTWaitOnAnimationEnd : BTBaseNode
{
    public override string s_DisplayName => "Waiting on ending of the animation";

    private Animator a_Animator;
    private BTWaitTask btwt_WaitNode;

    public BTWaitOnAnimationEnd(Blackboard _blackboard) : base(_blackboard)
    {
        a_Animator = b_BlackBoard.Get<Animator>("Animator");
    }

    public override TaskStatus OnEnter()
    {
        float currentClipLength = a_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        btwt_WaitNode = new BTWaitTask(b_BlackBoard, currentClipLength);

        return TaskStatus.Success;
    }

    public override TaskStatus OnUpdate()
    {
        return btwt_WaitNode.Evaluate();
    }
}
