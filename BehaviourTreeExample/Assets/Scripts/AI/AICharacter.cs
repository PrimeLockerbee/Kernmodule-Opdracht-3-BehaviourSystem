using UnityEngine;
using UnityEngine.AI;
using TMPro;

public abstract class AICharacter : Character
{
    [SerializeField] protected TMP_Text tmp_StateText;

    protected BTBaseNode bbn_Tree;
    protected NavMeshAgent nma_Agent;
    protected Animator a_Animator;

    protected virtual void Start()
    {
        InitializeBlackBoard();
    }

    protected abstract void InitializeBlackBoard();

    protected virtual void FixedUpdate()
    {
        TaskStatus? status = bbn_Tree?.Evaluate();
        if(bbn_Tree != null)
        {
            SetStateText(bbn_Tree.s_DisplayName);
        }
    }

    protected void SetStateText(string _message)
    {
        tmp_StateText.text = _message;
    }
}
