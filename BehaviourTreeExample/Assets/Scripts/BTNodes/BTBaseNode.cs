public enum TaskStatus { Success, Failed, Running }
public abstract class BTBaseNode
{
    public abstract string s_DisplayName { get; }

    protected Blackboard b_BlackBoard;
    public float f_UtilityScore;
    private bool b_IsInitialized;

    public BTBaseNode(Blackboard _blackboard)
    {
        b_BlackBoard = _blackboard;
    }

    public virtual TaskStatus OnEnter()
    {
        return TaskStatus.Success;
    }

    public abstract TaskStatus OnUpdate();

    public virtual TaskStatus OnExit()
    {
        return TaskStatus.Success;
    }

    public TaskStatus Evaluate()
    {
        if(!b_IsInitialized)
        {
            OnEnter();
            b_IsInitialized = true;
        }
        TaskStatus status = OnUpdate();
        if(status != TaskStatus.Running)
        {
            OnExit();
            b_IsInitialized = false;
        }
        return status;
    }
}
