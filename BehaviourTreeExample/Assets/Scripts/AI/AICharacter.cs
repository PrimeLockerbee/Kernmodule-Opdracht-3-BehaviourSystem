using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class AICharacter : Character
{
    [SerializeField] protected TMP_Text stateText;

    protected BTBaseNode tree;
    protected NavMeshAgent agent;
    protected Animator animator;

    protected virtual void Start()
    {
        InitializeBlackboard();
    }

    protected abstract void InitializeBlackboard();

    protected virtual void FixedUpdate()
    {
        TaskStatus? status = tree?.Evaluate();

        DecideStateMessage();
    }

    private void DecideStateMessage()
    {
        string message = blackBoard.Get<string>("StateMessage");
        if (message != default(string)) SetStateText(message);
        else if (tree != null) SetStateText(tree.displayName);
    }

    protected void SetStateText(string _message)
    {
        stateText.text = _message;
    }
}
