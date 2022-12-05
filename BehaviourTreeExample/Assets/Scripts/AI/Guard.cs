using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : AICharacter
{
    [HideInInspector] public Weapon w_Weapon;

    [SerializeField] private List<Transform> l_PatrolPoints;
    [SerializeField] private float f_MinDistance;

    [SerializeField] private float f_ChaseRange = 5;
    [SerializeField] private float f_AttackRange = 2;

    [SerializeField] private float maxWeaponDistance = 25;
    [SerializeField] private AnimationCurve ac_DamageEvaluator;
    [SerializeField] private AnimationCurve ac_DistanceEvaluator;

    [Range(-0.5f, 1f)]
    [SerializeField] private float f_IsInsightRange;

    [SerializeField] private float f_AnimationFadeTime;

    private void Awake()
    {
        nma_Agent = GetComponent<NavMeshAgent>();
        a_Animator = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();

        List<Vector3> patrolPosPoints = l_PatrolPoints.Select(t => t.position).ToList();
        Transform player = FindObjectOfType<Player>().transform;

        IsSpotable is_PlayerSpottable = player.GetComponent<IsSpotable>();

        BTBaseNode attackSequence = new BTSequence(b_BlackBoard, new BTIsTargetInRange(b_BlackBoard, player, f_AttackRange),new BTSequence(b_BlackBoard, new BTAnimate(b_BlackBoard, "Kick"),new BTWaitOnAnimationEnd(b_BlackBoard)));

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

        BTBaseNode patrolTree = new BTSequence(b_BlackBoard, new BTTargetSpot(b_BlackBoard, is_PlayerSpottable, false), new BTAnimate(b_BlackBoard, "Rifle Walk", f_AnimationFadeTime), new BTPatrolNode(b_BlackBoard, f_MinDistance, patrolPosPoints.ToArray()));

        bbn_Tree = new BTSelector(b_BlackBoard, findWeaponSequence, patrolTree);

        bbn_Tree = attackSequence;
    }

    protected override void InitializeBlackBoard()
    {
        b_BlackBoard = new Blackboard();

        b_BlackBoard.AddOrUpdate("Base", this);
        b_BlackBoard.AddOrUpdate("ControllerTransform", transform);
        b_BlackBoard.AddOrUpdate("Agent", nma_Agent);
        b_BlackBoard.AddOrUpdate("Animator", a_Animator);

        b_BlackBoard.AddOrUpdate("DistanceCurve", ac_DistanceEvaluator);
        b_BlackBoard.AddOrUpdate("DamageCurve", ac_DamageEvaluator);

        b_BlackBoard.AddOrUpdate("AttackRange", f_AttackRange);
    }
}
