using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : AICharacter
{
    public Weapon weapon { get; set; }

    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private float minDistance;

    [SerializeField] private float animationFadeTime;

    private float currentInSightRange = 20.0f;

    [Range(-0.5f, 1f)]
    [SerializeField] private float patrollingInSightRange;
    [Range(-1f, 1f)]
    [SerializeField] private float chasingInSightRange;

    [SerializeField] private float chaseRange = 10.0f;

    private Transform player;

    [SerializeField] private float maxWeaponDistance = 25;

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

        BTBaseNode moveToWeapon = new BTMoveToWeapon(blackBoard, maxWeaponDistance);
        BTBaseNode pickUpWeapon = new BTPickupWeapon(blackBoard);

        BTBaseNode checkForWeapon = new BTCheckForWeapon(blackBoard);

        BTBaseNode checkPlayerInSight = new BTSequence(blackBoard,
                   new BTCustomCondition(blackBoard, () => player.gameObject.activeSelf),
                   new BTIsTargetInRange(blackBoard, player, chaseRange),
                   new BTIsTargetInSight(blackBoard, player, currentInSightRange),
                   new BTSpotTarget(blackBoard, playerSpottable, true),
                   checkForWeapon
                   );

        BTBaseNode patrolNode = GeneratePatrolNode();

        BTBaseNode patrolTree = new BTSequence(blackBoard,
                   new BTChangeBlackBoardVariable(blackBoard, "StateMessage", "Patrolling"),
                   new BTInvokeAction(blackBoard, () => currentInSightRange = patrollingInSightRange),
                   new BTAnimate(blackBoard, "Rifle Walk", animationFadeTime),
                   patrolNode
                   );

        tree = new BTParralel(blackBoard,
               new BTSelector(blackBoard,
               checkPlayerInSight,
               patrolTree
               ),
               new BTSelector(blackBoard,
               new BTSequence(blackBoard,
               new BTIsTargetInSight(blackBoard, player, currentInSightRange),
               new BTSequence(blackBoard,
               new BTSpotTarget(blackBoard, playerSpottable, true))),
               new BTSpotTarget(blackBoard, playerSpottable, false))
               );
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // Update ControllerTransform in the blackboard
        blackBoard.AddOrUpdate("ControllerTransform", transform);
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

        blackBoard.AddOrUpdate("OriginalPatrolPosition", transform.position);

        float maxWeaponSearchDistance = 15.0f;
        blackBoard.AddOrUpdate("MaxSearchDistance", maxWeaponSearchDistance);

        blackBoard.AddOrUpdate("HasFoundWeapon", false);
    }
}
