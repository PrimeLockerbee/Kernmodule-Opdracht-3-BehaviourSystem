using UnityEngine;

public class BTPatrolNode : BTBaseNode
{
    public override string s_DisplayName => "Is patrolling";

    private Vector3[] v_Waypoints;
    private float f_MinDistance;

    private BTSequence bts_SequenceNode;

    public BTPatrolNode(Blackboard _blackboard, float _minDistance, params Vector3[] _waypoints) : base(_blackboard)
    {
        f_MinDistance = _minDistance;
        v_Waypoints = _waypoints;
    }

    public override TaskStatus OnEnter()
    {
        BTMoveToPosition[] children = new BTMoveToPosition[v_Waypoints.Length];
        for (int i = 0; i < v_Waypoints.Length; i++)
        {
            children[i] = new BTMoveToPosition(b_BlackBoard, v_Waypoints[i], f_MinDistance);
        }

        bts_SequenceNode = new BTSequence(b_BlackBoard, children);

        return bts_SequenceNode.OnEnter();
    }

    public override TaskStatus OnUpdate()
    {
        return bts_SequenceNode.Evaluate();
    }

    public override TaskStatus OnExit()
    {
        return bts_SequenceNode.OnExit();
    }
}
