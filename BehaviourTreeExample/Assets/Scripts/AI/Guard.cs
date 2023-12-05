using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : AICharacter
{
    public bool isAlerted { get; private set; }

    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private float minDistance;

    [SerializeField] private float animationFadeTime;

    private float currentInSightRange;

    [Range(-0.5f, 1f)]
    [SerializeField] private float patrollingInSightRange;

    [SerializeField] private float spotPlayerRange;

    private Transform player;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<Player>().transform;
    }

    protected override void Start()
    {
        base.Start();
        currentInSightRange = patrollingInSightRange;

        BTBaseNode patrolNode = GeneratePatrolNode();
        BTBaseNode attackPlayerNode = GenerateAttackPlayerNode();
        BTBaseNode returnToPatrolNode = GenerateReturnToPatrolNode();

        BTBaseNode patrolTree = new BTSequence(blackBoard,
                            new BTChangeBlackBoardVariable(blackBoard, "StateMessage", "Patrolling"),
                            new BTInvokeAction(blackBoard, () => currentInSightRange = patrollingInSightRange),
                            new BTAnimate(blackBoard, "Rifle Walk", animationFadeTime),
                            patrolNode);

        tree = new BTSelector(blackBoard,
               attackPlayerNode,
               returnToPatrolNode,
               patrolNode
              );

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private BTBaseNode GeneratePatrolNode()
    {
        List<Vector3> patrolPositions = patrolPoints.Select(t => t.position).ToList();

        BTMoveToPosition[] children = new BTMoveToPosition[patrolPositions.Count];
        for (int i = 0; i < patrolPositions.Count; i++)
        {
            children[i] = new BTMoveToPosition(blackBoard, patrolPositions[i], minDistance);
        }

        return new BTSequenceNoReset(blackBoard, children);
    }

    private BTBaseNode GenerateReturnToPatrolNode()
    {
        Vector3 originalPatrolPosition = blackBoard.Get<Vector3>("OriginalPatrolPosition");

        BTBaseNode returnToPatrol = new BTMoveToPosition(blackBoard, originalPatrolPosition, minDistance);

        return new BTSequence(blackBoard,
            new BTChangeBlackBoardVariable(blackBoard, "StateMessage", "ReturningToPatrol"),
            returnToPatrol
        );
    }

    private BTBaseNode GenerateAttackPlayerNode()
    {
        BTBaseNode checkForWeapon = new BTCheckForWeapon(blackBoard);
        BTBaseNode moveToPlayer = new BTMoveToPosition(blackBoard, player.position, minDistance);
        float attackRange = 10.0f; // Replace with actual attack range
        BTBaseNode attackPlayer = new BTIsTargetInRange(blackBoard, player.transform, attackRange);

        return new BTSequence(blackBoard,
            new BTChangeBlackBoardVariable(blackBoard, "StateMessage", "SearchingForWeapon"),
            checkForWeapon,
            moveToPlayer,
            attackPlayer
        );
    }

    protected override void InitializeBlackboard()
    {
        blackBoard = new Blackboard();

        blackBoard.AddOrUpdate("Base", this);
        blackBoard.AddOrUpdate("ControllerObject", gameObject);
        blackBoard.AddOrUpdate("ControllerTransform", transform);
        blackBoard.AddOrUpdate("Agent", agent);
        blackBoard.AddOrUpdate("Animator", animator);

        blackBoard.AddOrUpdate("OriginalPatrolPosition", transform.position);
    }
}
