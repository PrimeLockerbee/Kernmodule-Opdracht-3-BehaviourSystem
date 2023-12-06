using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckForWeapon : BTBaseNode
{
    public override string displayName => "Checking for weapon";

    public BTCheckForWeapon(Blackboard _blackBoard) : base(_blackBoard)
    {
    }

    public override TaskStatus OnUpdate()
    {
        Transform weaponTarget = FindWeapon();

        if (weaponTarget != null)
        {
            Vector3 weaponTargetPosition = DetermineWeaponTargetPosition(weaponTarget);
            blackboard.AddOrUpdate("WeaponTargetPosition", weaponTargetPosition);
            blackboard.AddOrUpdate("WeaponType", weaponTarget.GetComponent<Weapon>());
            blackboard.AddOrUpdate("HasFoundWeapon", true); // Update the flag

            Debug.Log("Weapon found!");
            return TaskStatus.Success;
        }
        else
        {
            Debug.Log("No weapon found.");
            return TaskStatus.Failed;
        }
    }

    private Vector3 DetermineWeaponTargetPosition(Transform weaponTransform)
    {
        return weaponTransform.position;
    }

    private Transform FindWeapon()
    {
        Transform controllerTransform = blackboard.Get<Transform>("ControllerTransform");
        float maxSearchDistance = blackboard.Get<float>("MaxSearchDistance");
        float searchRadius = 50.0f; // Adjust the radius based on your game's requirements

        Collider[] colliders = Physics.OverlapSphere(controllerTransform.position, searchRadius);

        Transform closestWeapon = null;
        float closestDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Weapon"))
            {
                float distance = Vector3.Distance(controllerTransform.position, collider.transform.position);

                // Check if this weapon is closer than the current closest weapon
                if (distance < closestDistance)
                {
                    closestWeapon = collider.transform;
                    closestDistance = distance;
                }
            }
        }

        if (closestWeapon != null)
        {
            Debug.Log("Closest weapon found!");
            return closestWeapon;
        }
        else
        {
            Debug.Log("No weapon found.");
            return null;
        }
    }
}