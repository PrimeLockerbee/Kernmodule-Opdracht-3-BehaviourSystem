using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPickupWeapon : BTBaseNode
{
    public override string displayName => "Picking up Weapon";

    private readonly System.Func<Transform> weaponTargetFunc;

    public BTPickupWeapon(Blackboard blackboard, System.Func<Transform> _weaponTargetFunc) : base(blackboard)
    {
        weaponTargetFunc = _weaponTargetFunc;
    }

    public override TaskStatus OnUpdate()
    {
        Transform weaponTarget = weaponTargetFunc.Invoke();

        if (weaponTarget != null)
        {
            // Implement logic to pick up the weapon
            Debug.Log("Picking up weapon!");

            // You can add your specific logic here for picking up the weapon

            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
    }
}
