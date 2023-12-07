using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToWeapon : BTBaseNode
{
    public override string displayName => "Moving to weapon";

    private NavMeshAgent agent;

    public BTMoveToWeapon(Blackboard _blackBoard) : base(_blackBoard)
    {
        agent = blackboard.Get<NavMeshAgent>("Agent");
    }

    public override TaskStatus OnUpdate()
    {
        Transform weaponTransform = blackboard.Get<Transform>("WeaponTargetPosition");

        if (weaponTransform != null)
        {
            agent.SetDestination(weaponTransform.position);

            // Check if the guard has reached the weapon position
            if (Vector3.Distance(agent.transform.position, weaponTransform.position) < 1.0f)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        return TaskStatus.Failed;
    }
}
