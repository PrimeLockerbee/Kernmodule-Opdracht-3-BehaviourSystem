using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    private enum BehaviorState
    {
        Patrol,
        DetectPlayer,
        SearchWeapon,
        PursuePlayer,
        AttackPlayer,
        ReturnToPatrol
    }

    private BehaviorState currentBehavior;
    private Transform playerTransform;
    private Transform originalPosition;
    private float patrolSpeed = 2f;
    private float pursueSpeed = 5f;
    private float attackRange = 2f;
    private float returnToPatrolDistance = 5f;

    // Define your waypoints for patrolling
    private List<Transform> waypoints;

    void Start()
    {
        // Initialize waypoints, playerTransform, and originalPosition

        // Start with patrolling behavior
        currentBehavior = BehaviorState.Patrol;
    }

    void Update()
    {
        // Update the behavior tree based on the current state
        switch (currentBehavior)
        {
            case BehaviorState.Patrol:
                Patrol();
                break;

            case BehaviorState.DetectPlayer:
                DetectPlayer();
                break;

            case BehaviorState.SearchWeapon:
                SearchWeapon();
                break;

            case BehaviorState.PursuePlayer:
                PursuePlayer();
                break;

            case BehaviorState.AttackPlayer:
                AttackPlayer();
                break;

            case BehaviorState.ReturnToPatrol:
                ReturnToPatrol();
                break;
        }
    }

    void Patrol()
    {
        // Implement your patrolling logic using waypoints
        // Move the enemy towards the next waypoint
        // If the player is detected, switch to DetectPlayer state
    }

    void DetectPlayer()
    {
        // Implement logic to detect the player
        // If the player is detected, switch to SearchWeapon state
        // If the player is not detected, switch to Patrol state
    }

    void SearchWeapon()
    {
        // Implement logic to search for a nearby weapon
        // If a weapon is found, switch to PursuePlayer state
        // If no weapon is found, switch to Patrol state
    }

    void PursuePlayer()
    {
        // Implement logic to pursue the player
        // If the player is within attack range, switch to AttackPlayer state
        // If the player runs away, and the distance exceeds a threshold, switch to ReturnToPatrol state
    }

    void AttackPlayer()
    {
        // Implement logic to attack the player
        // If the player dies or runs away, switch to ReturnToPatrol state
    }

    void ReturnToPatrol()
    {
        // Implement logic to return to the original patrol position
        // If the enemy reaches the original position, switch to Patrol state
    }
}
