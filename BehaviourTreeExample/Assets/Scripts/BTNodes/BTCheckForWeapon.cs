using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckForWeapon : BTBaseNode
{
    public override string displayName => "Checking for weapon";

    public BTCheckForWeapon(Blackboard blackboard) : base(blackboard)
    {
    }

    public override TaskStatus OnUpdate()
    {
        Transform weaponTarget = FindWeapon();

        if (weaponTarget != null)
        {
            // Determine the weapon target position
            Vector3 weaponTargetPosition = DetermineWeaponTargetPosition(weaponTarget);

            // Store the weapon target position in the blackboard
            blackboard.AddOrUpdate("WeaponTargetPosition", weaponTargetPosition);

            // Now you have the weapon target and its position
            // You can perform other actions or use this information in your behavior tree

            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
    }

    private Vector3 DetermineWeaponTargetPosition(Transform weaponTransform)
    {
        // Assuming the weaponTransform.position is the position you want to use
        return weaponTransform.position;
    }

    private Transform FindWeapon()
    {
        // Implement your logic to find the weapon
        // For example, you might use Physics.Raycast or other methods

        RaycastHit hit;
        if (Physics.Raycast(blackboard.Get<Transform>("ControllerTransform").position,
                            blackboard.Get<Transform>("ControllerTransform").forward, out hit))
        {
            return hit.transform;
        }

        return null;
    }
}