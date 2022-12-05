using UnityEngine;

public class BTAnimate : BTBaseNode
{
    public override string s_DisplayName => $"Animation ({s_Clip})";

    private Animator a_Animator;
    private string s_Clip;
    private float f_FadeTime;

    public BTAnimate(Blackboard _blackboard, string _clip, float _fadeTime = 0f) : base(_blackboard)
    { 
        s_Clip = _clip;
        f_FadeTime = _fadeTime;
        a_Animator = b_BlackBoard.Get<Animator>("Animator");
    }

    public override TaskStatus OnEnter()
    {
        SwitchAnimation(s_Clip, f_FadeTime);

        return base.OnEnter();
    }

    private void SwitchAnimation(string _clipName, float _fadeTime)
    {
        if (!a_Animator.GetCurrentAnimatorStateInfo(0).IsName(_clipName) && !a_Animator.IsInTransition(0))
        {
            a_Animator.CrossFade(_clipName, _fadeTime);
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
