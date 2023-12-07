using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPickupWeapon : BTBaseNode
{
    public override string displayName => "Picking up Weapon";

    Guard guardReference;

    public BTPickupWeapon(Blackboard _blackboard) : base(_blackboard)
    {
        guardReference = blackboard.Get<Guard>("Base");
    }

    public override TaskStatus OnEnter()
    {
        Weapon weapon = blackboard.Get<Weapon>("BestWeapon");
        guardReference.weapon = weapon;

        if (weapon == null) return TaskStatus.Failed;
        else
        {
            // Deactivate the weapon and its parent object
            weapon.gameObject.SetActive(false);

            return TaskStatus.Success;
        }
    }

    public override TaskStatus OnUpdate()
    {
        // The guard has successfully picked up the weapon
        return TaskStatus.Success;
    }

}
