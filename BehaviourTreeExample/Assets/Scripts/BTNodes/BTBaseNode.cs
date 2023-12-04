public enum TaskStatus { Success, Failed, Running }

public abstract class BTBaseNode
{
    public abstract string displayName { get; }

    protected Blackboard blackboard;
    private bool isInitialized;

    public BTBaseNode(Blackboard _blackboard)
    {
        blackboard = _blackboard;
    }

    public virtual TaskStatus OnEnter() { return TaskStatus.Success; }
    public abstract TaskStatus OnUpdate();
    public virtual TaskStatus OnExit() { return TaskStatus.Success; }
    public virtual void Abort() { isInitialized = false; }

    public TaskStatus Evaluate()
    {
        if (!isInitialized)
        {
            OnEnter();
            isInitialized = true;
        }
        TaskStatus status = OnUpdate();
        if (status != TaskStatus.Running)
        {
            OnExit();
            isInitialized = false;
        }
        return status;
    }
}
