using UnityEngine;

public class BTAnimate : BTBaseNode
{
    public override string displayName => $"Animation ({clip})";

    private Animator animator;
    private string clip;
    private float fadeTime;

    public BTAnimate(Blackboard _blackboard, string _clip, float _fadeTime = 0f) : base(_blackboard)
    {
        clip = _clip;
        fadeTime = _fadeTime;
        animator = blackboard.Get<Animator>("Animator");
    }

    public override TaskStatus OnEnter()
    {
        return base.OnEnter();
    }

    private void SwitchAnimation(string _clipName, float _fadeTime)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(_clipName) /*&& !animator.IsInTransition(0)*/)
        {
            animator.CrossFade(_clipName, _fadeTime);
        }
    }

    public override TaskStatus OnUpdate()
    {
        SwitchAnimation(clip, fadeTime);

        return TaskStatus.Success;
    }
}
