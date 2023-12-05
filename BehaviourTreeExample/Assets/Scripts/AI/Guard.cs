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

        BTBaseNode attackSequence = new BTSequence(b_BlackBoard, new BTIsTargetInRange(b_BlackBoard, player, f_AttackRange), new BTSequence(b_BlackBoard, new BTAnimate(b_BlackBoard, "Kick"), new BTWaitOnAnimationEnd(b_BlackBoard)));
        BTBaseNode chasingTree = new BTParallel(b_BlackBoard, new BTTargetFollow(b_BlackBoard, player, f_AttackRange), attackSequence);
        BTBaseNode whileInSight = new BTConditionDecorator(b_BlackBoard, new BTIsTargetInSight(b_BlackBoard, player, f_IsInsightRange), chasingTree);
        BTBaseNode findWeaponSequence = new BTSequence(b_BlackBoard,
                                            new BTIsTargetInRange(b_BlackBoard, player, f_ChaseRange),
                                            new BTIsTargetInSight(b_BlackBoard, player, f_IsInsightRange),
                                            new BTTargetSpot(b_BlackBoard, is_PlayerSpottable, true),
                                            new BTSelector(b_BlackBoard,
                                                new BTSequence(b_BlackBoard,
                                                    new BTInvert(b_BlackBoard,
                                                        new BTWeaponTaken(b_BlackBoard, this)
                                                    ),
                                                    new BTWeaponSeeker(b_BlackBoard, maxWeaponDistance),
                                                    new BTMoveToBlackBoardPosition(b_BlackBoard, "Best Weapon Position", f_MinDistance),
                                                    new BTPickupWeapon(b_BlackBoard),
                                                    whileInSight
                                                )
                                            ));

        BTBaseNode patrolTree = new BTSequence(blackBoard,
                            new BTChangeBlackBoardVariable(blackBoard, "StateMessage", "Patrolling"),
                            new BTInvokeAction(blackBoard, () => currentInSightRange = patrollingInSightRange),
                            new BTAnimate(blackBoard, "Rifle Walk", animationFadeTime),
                            patrolNode);

        tree = new BTSelector(blackBoard,
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

        blackBoard.AddOrUpdate("Base", this);
        blackBoard.AddOrUpdate("ControllerObject", gameObject);
        blackBoard.AddOrUpdate("ControllerTransform", transform);
        blackBoard.AddOrUpdate("Agent", agent);
        blackBoard.AddOrUpdate("Animator", animator);
    }
}
