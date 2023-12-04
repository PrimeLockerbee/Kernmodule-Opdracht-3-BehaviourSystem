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

    private Transform player;
    private float currentInSightRange;

    [Range(-0.5f, 1f)]
    [SerializeField] private float patrollingInSightRange;
    [Range(-1f, 1f)]
    [SerializeField] private float chasingInSightRange;

    [SerializeField] private float chaseRange = 5;

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

        ISpottable playerSpottable = player.GetComponent<ISpottable>();

        BTBaseNode patrolNode = GeneratePatrolNode();

        BTBaseNode patrolTree = new BTSequence(blackBoard,
                            new BTChangeBlackBoardVariable(blackBoard, "StateMessage", "Patrolling"),
                            new BTInvokeAction(blackBoard, () => currentInSightRange = patrollingInSightRange),
                            new BTAnimate(blackBoard, "Rifle Walk", animationFadeTime),
                            patrolNode);

        BTBaseNode playerInGuardSight = new BTSequence(blackBoard, new BTIsTargetInRange(blackBoard, player, chaseRange),
                                                new BTIsTargetInSight(blackBoard, player, currentInSightRange),
                                                new BTSpotTarget(blackBoard, playerSpottable, true));

        tree = new BTSelector(blackBoard,
              playerInGuardSight,
              patrolTree
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

    protected override void InitializeBlackboard()
    {
        blackBoard = new Blackboard();

        // Components
        blackBoard.AddOrUpdate("Base", this);
        blackBoard.AddOrUpdate("ControllerObject", gameObject);
        blackBoard.AddOrUpdate("ControllerTransform", transform);
        blackBoard.AddOrUpdate("Agent", agent);
        blackBoard.AddOrUpdate("Animator", animator);
    }
}
