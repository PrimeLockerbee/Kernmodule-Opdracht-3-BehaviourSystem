using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPickupWeapon : BTBaseNode
{
    public override string displayName => "Picking up weapon";

    private Guard guardReference;
    private Transform weaponTransform;

    public BTPickupWeapon(Blackboard _blackboard, Transform _weaponTransform) : base(_blackboard)
    {
        guardReference = blackboard.Get<Guard>("Base");
        weaponTransform = _weaponTransform;
    }

    public override TaskStatus OnEnter()
    {
        guardReference.weapon = weaponTransform.GetComponent<Weapon>();

        if (guardReference.weapon == null)
            return TaskStatus.Failed;
        else
        {
            Rigidbody rb = guardReference.weapon.GetComponent<Rigidbody>();
            if (rb != null) rb.useGravity = false;

            // Assuming the weapon is a child of a weapon holder object
            Transform weaponHolder = weaponTransform.parent;
            weaponTransform.parent = null;
            weaponTransform.rotation = Quaternion.identity;

            if (weaponHolder != null)
                weaponHolder.gameObject.SetActive(false);

            return TaskStatus.Success;
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
